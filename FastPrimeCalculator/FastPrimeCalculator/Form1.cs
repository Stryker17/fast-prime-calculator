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

        // the 
        private System.Threading.Timer calculationTimer = null;

        // we'll perform the actual calculation on a thread separate from the UI thread
        private Thread primeNumberCalculationThread = null;

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
        private void UpdatePrimeNumber(UInt64 primeNumber, UInt64 numPrimes)
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
            UInt64 ValueTestedForPrime = 2;
            UInt64 i = 0;
            UInt64 j = 0;

            // Calculation will run 'forever' until instructed to stop via timer or user interrupt.
            // This first attempt is a simple brute-force, single-threaded calculation but we can
            // immediately leverage it into a unit test once boilerplate prototyping is completed
            // to ensure optimizations still produce a valid calculation of prime numbers
            while (true)
            {
                for (i = 2; i < ValueTestedForPrime; i++)
                {
                    if (ValueTestedForPrime % i == 0)
                    {
                        // Value under test is divisible by a smaller number, therefore NOT prime
                        break;
                    }
                }
                if(ValueTestedForPrime == i)
                {
                    j++;
                    // PRIME!!!
                    BeginInvoke(new Action<UInt64,UInt64>(UpdatePrimeNumber), new object[2] { i, j });
                }

                ValueTestedForPrime++;
            }
        }

        /// <summary>
        /// This method will finish the prime number calculation cycle and reset the UI buttons.
        /// </summary>
        protected void FinishCalculation()
        {
            primeNumberCalculationThread.Abort();
            calculationTimer.Change(Timeout.Infinite, Timeout.Infinite);
            BeginInvoke(new Action(ResetButtonState), null);
        }

    }
}
