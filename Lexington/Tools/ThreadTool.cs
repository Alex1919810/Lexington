namespace Lexington.Tools
{
    public class ThreadTool
    {

        public class AsyncLock : IDisposable
        {
            private SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

            // 异步获取锁
            public async Task<IDisposable> LockAsync()
            {
                await semaphore.WaitAsync();
                return new LockReleaser(semaphore);
            }

            // 同步获取锁
            public IDisposable Lock()
            {
                return new LockReleaser(semaphore);
            }

            public void Dispose()
            {
                semaphore.Dispose();
            }

            private class LockReleaser : IDisposable
            {
                private SemaphoreSlim semaphore;

                public LockReleaser(SemaphoreSlim semaphore)
                {
                    this.semaphore = semaphore;
                }

                public void Dispose()
                {
                    semaphore.Release();
                }
            }
        }
    }
}
