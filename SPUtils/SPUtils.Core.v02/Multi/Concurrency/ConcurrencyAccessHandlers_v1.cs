using System;
using System.Threading;

namespace SPUtils.Core.v02.Multi.Concurrency
{
    public class SyncdAccessHandler_SpinLock
    {
        private SpinLock _synchronizeAccessLock;

        public SyncdAccessHandler_SpinLock()
        {
            _synchronizeAccessLock = new SpinLock();
        }

        public void SafeExecute(Action criticalAction)
        {
            bool validateLock = false;
            
            _synchronizeAccessLock.Enter(ref validateLock);

            if (!validateLock)
                throw new SynchronizationLockException("Could not obtain lock");

            try
            {
                //Perform the critical action
                criticalAction();
            }
            finally
            {
                _synchronizeAccessLock.Exit();
            }
        }

        public void SafeExecute(Action criticalAction, TimeSpan timeout)
        {
            bool validateLock = false;

            _synchronizeAccessLock.TryEnter((int)timeout.TotalMilliseconds, ref validateLock);

            if (!validateLock)
                throw new SynchronizationLockException("Could not obtain lock");

            try
            {
                //Perform the critical action
                criticalAction();
            }
            finally
            {
                _synchronizeAccessLock.Exit();
            }
        }

        public T SafeExecute<T>(Func<T> criticalAction)
        {
            bool validateLock = false;

            _synchronizeAccessLock.Enter(ref validateLock);

            if (!validateLock)
                throw new SynchronizationLockException("Could not obtain lock");

            try
            {
                //Perform the critical action
                return criticalAction();
            }
            finally
            {
                _synchronizeAccessLock.Exit();
            }
        }

        public bool IsTaken()
        {
            bool validateLock = false;

            _synchronizeAccessLock.Enter(ref validateLock);

            if (validateLock)
            {
                _synchronizeAccessLock.Exit();
                return false;
            }

            return true;
        }
    }

    public class SyncdAccessHandler_Monitor
    {
        private object _lockObject = null;

        public SyncdAccessHandler_Monitor()
        {
            _lockObject = new object();
        }

        public void SafeExecute(Action criticalAction)
        {
            bool lockWasTaken = false;
            
            try
            {
                //Try to acquire lock
                Monitor.Enter(_lockObject, ref lockWasTaken);

                //Perform the critical action;
                criticalAction();
            }
            finally
            {
                //Release monitor lock if was taken
                if(lockWasTaken)
                    Monitor.Exit(_lockObject);
            }
        }

        public bool SafeExecute(Action criticalAction, TimeSpan timeout)
        {
            bool lockWasTaken = false;

            try
            {
                //Try to acquire lock without freezing
                Monitor.TryEnter(_lockObject, timeout, ref lockWasTaken);

                if (lockWasTaken)
                {
                    //Perform the critical action;
                    criticalAction();
                    return true;
                }

                //Since we couldn't acquire the lock we need to indicate that the operation failed
                return false;
            }
            finally
            {
                //Release monitor lock if was taken
                if (lockWasTaken)
                    Monitor.Exit(_lockObject);
            }
        }

        public T SafeExecute<T>(Func<T> criticalAction)
        {
            bool lockWasTaken = false;

            try
            {
                //Try to acquire lock
                Monitor.Enter(_lockObject, ref lockWasTaken);

                //Perform the critical action;
                return criticalAction();
            }
            finally
            {
                //Release monitor lock if was taken
                if (lockWasTaken)
                    Monitor.Exit(_lockObject);
            }
        }

        public bool IsTaken()
        {
            //Try to acquire lock
            if(Monitor.TryEnter(_lockObject))
            {
                //If acquired then release and return state accordingly
                Monitor.Exit(_lockObject);
                return false;
            }

            //If not it means that its already held so return state accordingly
            return true;
        }        
    }

    public class SyncdAccessHandler_ReaderWriter
    {
        private ReaderWriterLock _readerWriterLock = null;
        private TimeSpan DEFAULT_WAIT_TIME_SPAN = TimeSpan.FromMinutes(30);

        public SyncdAccessHandler_ReaderWriter()
        {
            _readerWriterLock = new ReaderWriterLock();
        }

        public void SafeRead(Action criticalReadAction, double timeoutSpanMillis = -1.0)
        {
            TimeSpan timeout;

            if (timeoutSpanMillis != -1.0)
                timeout = TimeSpan.FromMilliseconds(timeoutSpanMillis);
            else
                timeout = DEFAULT_WAIT_TIME_SPAN;

            _readerWriterLock.AcquireReaderLock(timeout);
            
            try
            {
                criticalReadAction();
            }
            finally
            {
                _readerWriterLock.ReleaseReaderLock();
            }
        }

        public T SafeRead<T>(Func<T> criticalReadAction, double timeoutSpanMillis = -1.0)
        {
            TimeSpan timeout;

            if (timeoutSpanMillis != -1.0)
                timeout = TimeSpan.FromMilliseconds(timeoutSpanMillis);
            else
                timeout = DEFAULT_WAIT_TIME_SPAN;

            _readerWriterLock.AcquireReaderLock(timeout);

            try
            {            
                return criticalReadAction();
            }
            finally
            {
                _readerWriterLock.ReleaseReaderLock();
            }
        }

        public void SafeWrite(Action criticalWriteAction, double timeoutSpanMillis = -1.0)
        {
            TimeSpan timeout;

            if (timeoutSpanMillis != -1.0)
                timeout = TimeSpan.FromMilliseconds(timeoutSpanMillis);
            else
                timeout = DEFAULT_WAIT_TIME_SPAN;

            _readerWriterLock.AcquireWriterLock(timeout);

            try
            {
                criticalWriteAction();
            }
            finally
            {
                _readerWriterLock.ReleaseWriterLock();
            }
        }

        public T SafeWrite<T>(Func<T> criticalWriteAction, double timeoutSpanMillis = -1.0)
        {
            TimeSpan timeout;

            if (timeoutSpanMillis != -1.0)
                timeout = TimeSpan.FromMilliseconds(timeoutSpanMillis);
            else
                timeout = DEFAULT_WAIT_TIME_SPAN;

            _readerWriterLock.AcquireWriterLock(timeout);

            try
            {
                return criticalWriteAction();
            }
            finally
            {
                _readerWriterLock.ReleaseWriterLock();
            }
        }
    }

    public class SyncdAccessHandler_ReaderWriterSlim
    {
        private ReaderWriterLockSlim _readerWriterLock = null;

        public SyncdAccessHandler_ReaderWriterSlim()
        {
            _readerWriterLock = new ReaderWriterLockSlim();
        }

        public void SafeRead(Action criticalReadAction)
        {
            _readerWriterLock.EnterReadLock();

            try
            {
                criticalReadAction();
            }
            finally
            {
                _readerWriterLock.ExitReadLock();
            }
        }

        public T SafeRead<T>(Func<T> criticalReadAction)
        {
            _readerWriterLock.EnterReadLock();

            try
            {
                return criticalReadAction();
            }
            finally
            {
                _readerWriterLock.ExitReadLock();
            }
        }

        public void SafeWrite(Action criticalWriteAction)
        { 
            _readerWriterLock.EnterWriteLock();

            try
            {
                criticalWriteAction();
            }
            finally
            {
                _readerWriterLock.ExitWriteLock();
            }
        }

        public T SafeWrite<T>(Func<T> criticalWriteAction)
        {
            _readerWriterLock.EnterWriteLock();

            try
            {
                return criticalWriteAction();
            }
            finally
            {
                _readerWriterLock.ExitWriteLock();
            }
        }
    }
}
