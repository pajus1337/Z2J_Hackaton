namespace Hackaton_001_PriorityQueue
{
    public class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            QueueObjectManager queueObjectManager = new QueueObjectManager();

            for (int i = 0; i < 10; i++)
            {
                queueObjectManager.AddObjToQueue(new QueueObject(random.Next(1, 5)));
            }
        }
    }

    public class QueueObject
    {
        private static int lastId = 0;
        public int Id { get; set; }
        public int Priority { get; set; }

        public QueueObject(int priority)
        {
            Priority = priority;
            Id = ++lastId;
        }
        public override string ToString()
        {
            return $"Object ID : {Id}, Object Priority Level : {Priority}";
        }
    }

    public class QueueObjectManager
    {
        private List<QueueObject> queueObjects = new List<QueueObject>();

        public void DisplayWholeQueueCollection()
        {
            foreach (var queueObj in queueObjects)
            {
                Console.WriteLine(queueObj);
            }
        }

        //Podstawowe metody kolejki(**które należy zaimplementować w ramach rozwiązania**) :
        //- Dodaj element na końcu kolejki
        public void AddObjToQueue(QueueObject queueObject)
        {
            queueObjects.Add(queueObject);
            SortCollection();
        }

        //- Pobierz i usuń element z początku kolejki
        public QueueObject Pop()
        {
            if (queueObjects.Count != 0)
            {
                var popedObject = queueObjects[0];
                queueObjects.RemoveAt(0);
                return popedObject;
            }
            return null;
        }

        //- Pobierz 1 element bez usuwania(podgląd)
        public QueueObject Peek()
        {
            if (queueObjects.Count != 0)
            {
                return queueObjects[0];
            }
            return null;
        }

        //- Czy kolejka jest pusta?
        public bool isQueueEmpty()
        {
            if (queueObjects.Count != 0)
            {
                return false;
            }
            return true;
        }

        //- Ile elementów jest w kolejce
        public int GetObjectCount()
        {
            return queueObjects.Count;
        }

        public void SortCollection()
        {
            bool isAnythingSwaped;
            do
            {
                isAnythingSwaped = false;
                for (int i = 0; i < queueObjects.Count - 1; i++)
                {
                    if (queueObjects[i].Priority < queueObjects[i + 1].Priority)
                    {
                        var lowPrio = queueObjects[i];
                        var highprio = queueObjects[i + 1];

                        queueObjects[i] = highprio;
                        queueObjects[i + 1] = lowPrio;
                        isAnythingSwaped = true;
                    }

                    else if (queueObjects[i].Priority == queueObjects[i + 1].Priority & queueObjects[i].Id > queueObjects[i + 1].Id)
                    {
                        var lowId = queueObjects[i + 1];
                        var highId = queueObjects[i];

                        queueObjects[i] = lowId;
                        queueObjects[i + 1] = highId;
                        isAnythingSwaped = true;
                    }
                }
            } while (isAnythingSwaped);
        }
    }
}

