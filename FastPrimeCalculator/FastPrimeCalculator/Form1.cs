using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastPrimeCalculator
{
    public partial class Form1 : Form
    {
        // Time remaining to perform prime number calculations
        UInt32 timeRemaining = 0;

        // the timer limiting how long we can calculate primes
        private System.Threading.Timer calculationTimer = null;

        // we'll perform the actual calculation on a thread separate from the UI thread
        private Thread primeNumberCalculationThread = null;

        private bool abortMainCalculationThread = false;

        /// <summary>
        /// Constructor for the basic form UI supporting this application
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            calculationTimer = new System.Threading.Timer(new TimerCallback(TimeLimitProc));

        }

        /// <summary>
        /// Reset the state of all buttons and values
        /// </summary>
        private void ResetButtonState()
        {
            AbortButton.Enabled = false;
            StartButton.Enabled = true;
        }

        private void UpdateTimerValue(UInt32 timeoutValue)
        {
            CalculationTimeout.Text = timeoutValue.ToString();
        }

        /// <summary>
        /// Procedure handler for the timeLimit event timer
        /// </summary>
        /// <param name="obj">not used</param>
        private void TimeLimitProc(object obj)
        {
            if (timeRemaining > 0)
            {
                // subtract one second from the timer
                timeRemaining--;

                BeginInvoke(new Action<UInt32>(UpdateTimerValue), new object[1] { timeRemaining });

                if (timeRemaining == 0)
                {
                    FinishCalculation();
                }
            }
        }

        /// <summary>
        /// Button click handler to Initiate the Prime Number calculation
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            // Ensure proper button usability now that we're in the 'running' state
            AbortButton.Enabled = true;
            StartButton.Enabled = false;

            abortMainCalculationThread = false;
            primeNumbers = new List<ulong>();

            UInt32.TryParse(CalculationTimeout.Text, out timeRemaining);

            // if the provided timeout is not a positive number, display an error and return
            if (timeRemaining <= 0)
            {
                TimerValueErrorMsg.Visible = true;
                ResetButtonState();
                return;
            }
            else
            {
                TimerValueErrorMsg.Visible = false;
            }

            calculationTimer.Change(1000, 1000);

            // Perform the calculation
            primeNumberCalculationThread = new Thread(CalculatePrimeNumbers);
            primeNumberCalculationThread.Start();
        }

        /// <summary>
        /// Button click handler to Abort the current Prime Number calculation
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void AbortButton_Click(object sender, EventArgs e)
        {
            FinishCalculation();
        }

        /// <summary>
        /// Update the form's Highest Prime Number text field.
        /// </summary>
        /// <param name="primeNumber">The highest prime number value</param>
        /// <param name="numPrimes">The total number of primes so far</param>
        private void UpdatePrimeNumber(UInt64 primeNumber, int numPrimes)
        {
            HighestPrimeNumber.Text = primeNumber.ToString();
            NumberOfPrimes.Text = numPrimes.ToString();
        }

        /// <summary>
        /// Main function for performing the calculation of prime numbers.
        /// This method will terminate upon reaching a designated time interval.
        /// It will run on a separate worker thread.
        /// </summary>
        protected void CalculatePrimeNumbers()
        {
            // Seed original tested value with 2 which is the lowest POSSIBLE prime number
            UInt64 valueTestedForPrime = 2;
            int i = 0;

            // save a prime numbers list to optimize calculations
            //List<UInt64> primeNumbers = new List<ulong>();

            Thread[] workerThreads = new Thread[Environment.ProcessorCount];

            // Calculation will run 'forever' until instructed to stop via timer or user interrupt.
            // this method utilizes a prime number lookup table to reduce the number of calculations required.
            // it also performs parallel processing on the lookup table above a certain threshold.
            // the threshold value simply reduces the multi-thread overhead when the table size
            // is small and single-threaded may be more efficient.
            while (true)
            {
                // work on single thread until it's efficient to move to parallel processing
                if (primeNumbers.Count < 20000)
                {
                    for (i = 0; i < primeNumbers.Count; i++)
                    {
                        if (valueTestedForPrime % primeNumbers[i] == 0)
                        {
                            // Value under test is divisible by a smaller number, therefore NOT prime
                            break;
                        }
                    }

                    if (abortMainCalculationThread)
                    {
                        // if we've aborted, don't add any more primes
                        return;
                    }

                    if (primeNumbers.Count == i)
                    {
                        // PRIME!!!
                        primeNumbers.Add(valueTestedForPrime);
                        BeginInvoke(new Action<UInt64, int>(UpdatePrimeNumber), new object[2] { valueTestedForPrime, primeNumbers.Count });
                    }
                }
                else
                {
                    int sectionSize = primeNumbers.Count / workerThreads.Length;
                    for (i = 0; i < workerThreads.Length - 1; i++)
                    {
                        workerThreads[i] = new Thread(new ParameterizedThreadStart(PrimeCalculationWorker));
                        object args = new object[4] { valueTestedForPrime, (i * sectionSize), (i * sectionSize + sectionSize - 1), i };
                        workerThreads[i].Start(args);
                    }
                    workerThreads[i] = new Thread(new ParameterizedThreadStart(PrimeCalculationWorker));
                    object argsEnd = new object[4] { valueTestedForPrime, (i * sectionSize), primeNumbers.Count - 1, i };
                    workerThreads[i].Start(argsEnd);

                    for (i = 0; i < workerThreads.Length; i++)
                    {
                        workerThreads[i].Join();
                    }

                    if (abortMainCalculationThread)
                    {
                        // if we've aborted, don't add any more primes
                        return;
                    }

                    if (workerThreadAppearsPrime.All(x => x))
                    {
                        // PRIME!!!
                        primeNumbers.Add(valueTestedForPrime);
                        BeginInvoke(new Action<UInt64, int>(UpdatePrimeNumber), new object[2] { valueTestedForPrime, primeNumbers.Count });
                    }

                    // reset the thread results to assume not prime (aka false)
                    for (int k=0; k<workerThreadAppearsPrime.Length; k++)
                    {
                        workerThreadAppearsPrime[k] = false;
                    }
                    abortWorkerThread = false;
                }

                valueTestedForPrime++;
            }
        }

        // save a prime numbers list to optimize calculations
        List<UInt64> primeNumbers = new List<ulong>();

        // boolean event to indicate we should abort the worker thread
        volatile bool abortWorkerThread = false;

        bool[] workerThreadAppearsPrime = new bool[Environment.ProcessorCount];

        /// <summary>
        /// Worker thread to calculate a portion of the prime numbers table
        /// <param name="args">arg1 is the value under test, arg2 is the starting position, arg3 is the ending position in the prime list comparison, arg4 is the thread number</param>
        /// </summary>
        void PrimeCalculationWorker(object args)
        {
            UInt64 valueTestedForPrime = (UInt64)((Array)args).GetValue(0);
            int start = (int)((Array)args).GetValue(1);
            int finish = (int)((Array)args).GetValue(2);
            int threadNumber = (int)((Array)args).GetValue(3);
            int i = 0;

            for(i=start; i <= finish; i++)
            {
                if (abortWorkerThread) break;

                if (valueTestedForPrime % primeNumbers[i] == 0)
                {
                    // Value under test is divisible by a smaller number, therefore NOT prime
                    abortWorkerThread = true;
                    break;
                }
            }
            if(i > finish)
            {
                // appears prime in this section
                workerThreadAppearsPrime[threadNumber] = true;
            }
        }


        /// <summary>
        /// This method will finish the prime number calculation cycle and reset the UI buttons.
        /// </summary>
        protected void FinishCalculation()
        {
            abortMainCalculationThread = true;
            abortWorkerThread = true;

            calculationTimer.Change(Timeout.Infinite, Timeout.Infinite);
            BeginInvoke(new Action(ResetButtonState), null);
        }

    }
}
