# fast-prime-calculator

methodology was to create boilerplate app that would update information to the UI (timer value, current highest prime number) and
incorporate a basic brute force prime number calculation.  this prime number calculation is simply the test for divisibility of any lesser values.

the UI is perhaps overdone as it offers a settable calculation timer and an Abort button.  One 'flaw', if it can be called that,
is that the UI form's text field BeginInvoke can't keep up with displaying the current highest prime number until (on my PC) after about 
14000+ primes have been calculated within the first second or two.  After that, the calculation of the larger primes is finally slow enough
for the UI to keep up.

the next step was to perform the prime number calculation only against earlier prime numbers rather than all numbers below
the currently tested number.  this optimization is common knowledge in prime number calculations and is the next logical step
in performing the optimization.  this was still only occurring on a single thread at this point and it was obvious
from debugging that CPU usage still had a lot of unutilized headroom.

following step is to perform scalable parallel processing of the prime number by creating threads to calculate only against
individual sections of the prime number table.  i initially did this with Thread class threads, which i could Join to the primary thread.
These threads would each indicate if their section APPEARED prime and if at some point they determined they weren't then
they could signal other threads to not bother completing their tests.  If ALL threads indicated this value appeared prime against
their section of the table, then it's a prime number and it gets added to the list.

I found that Thread class threads, due to instantiation time, were slower than ThreadPool threads but i needed to implement
a list of ManualResetEvents to be Set by each worker thread and a WaitAll in the primary thread since ThreadPool threads
don't have a handle upon which I could perform a Join.  This was annoying but did offer a performance improvement.

I tried to then implement this in C++, which i believe would likely produce a faster division calculation against the table,
but it's been a long time since I've done C++ and it was not coming together as easily as I'd hoped.

Lastly, I'd originally set the number of worker threads to the number of processors.  Unfortunately, there's no easy way to set managed
thread affinity to a given processor core, so i had to rely on .Net to do-the-right-thing.  Oddly, i found that doubling the 
worker thread count improved performance a little bit - creating 8 worker threads to be run against my 4 core processor.

implementing this with parallel processing is key to scalability in calculating the prime numbers, though i'm sure further
optimizations can be performed.  also, implementing in a faster language such as natively compiled C++ could see a speed improvement.

i've tried to keep the code well documented, and the commit history should show the evolution of the code.

highest prime number calculated on my PC is 2,569,691.  My counter shows this to be the 187,793rd prime number.