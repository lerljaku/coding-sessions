import threading

i: int = 0
lock = threading.RLock()


def thread_job(t):
    global i
    for x in range(100000):
        lock.acquire()
        i = i + 1
        lock.release()


t1 = threading.Thread(target=thread_job, args=(1,))
t2 = threading.Thread(target=thread_job, args=(2,))

t1.start()
t2.start()

print(i)




