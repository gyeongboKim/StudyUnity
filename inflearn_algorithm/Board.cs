using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace inflearn_algorithm
{
    #region MyList 구현
    //class MyList<T>
    //{
    //    const int DEFAULTSize = 1;
    //    T[] _data = new T[DEFAULTSize];        //Capacity와 일치

    //    public int Count  = 0;       //실제로 사용중인 데이터 개수
    //    public int Capacity { get { return _data.Length; } }    //예약된 데이터 개수
    //    //O(1)  //추가하는 경우는 상수 시간 복잡도로 함. (if의 이사비용은 무시. 두배로 늘어나기 때문에 작동 빈도가 적음으로 무시함.)
    //    public void Add(T item)
    //    {
    //        // 1. 공간이 충분한지 확인한다.
    //        // 사용중인 데이터가 예약된 데이터보다 많은 경우
    //        if (Count >= Capacity)
    //        {
    //            //공간을 다시 늘려서 확보
    //            T[] newArray = new T[Count * 2];
    //            for(int i = 0; i < Count; i++)
    //                newArray[i] = _data[i];
    //            _data = newArray;
    //        }
    //        // 2. 공간에 데이터 삽입
    //        _data[Count] = item;
    //        Count++;
    //    }

    //    //O(1)
    //    public T this[int index]
    //    {
    //        get { return _data[index]; }
    //        set { _data[index] = value; }
    //    }

    //    //O(n) for문이 데이터 크기에 비례
    //    // index 가 0이면 n번 => n
    //    // index 가 최대 크기면 1번 ==> 1
    //    // worst로 결국 O(n)이 됨.
    //    public void RemoveAt(int index)
    //    {
    //        for(int i = index; i  < Count -1; i++)
    //            _data[i] = _data[i + 1];

    //        _data[Count - 1] = default(T);

    //        Count--;
    //    }
    //}
    #endregion
    #region MyLinkedList
    //class MyLinkedListNode<T>
    //{
    //    public T Data;
    //    //주소값을 저장(참조)
    //    public MyLinkedListNode<T> Next;   
    //    public MyLinkedListNode<T> Prev;
    //}

    //class MyLinkedList<T>
    //{
    //    public MyLinkedListNode<T> Head = null;    //첫번째
    //    public MyLinkedListNode<T> Tail = null;    //마지막
    //    public int Count = 0;

    //    //O(1)
    //    public MyLinkedListNode<T> AddLast(T data)
    //    {
    //        MyLinkedListNode<T> newNode = new MyLinkedListNode<T>();
    //        newNode.Data = data;

    //        //만약 아직 노드가 아예 없다면 새로 추가한 첫 번째 노드가 Head이다.
    //        if (Head == null)
    //            Head = newNode;

    //        //기존의 마지막 노드와 새로 추가되는 노드를 연결한다.
    //        if(Tail != null)
    //        {
    //            Tail.Next = newNode;
    //            newNode.Prev = Tail;
    //        }
    //        //새로 추가되는 노드를 마지막 노드로 한다.
    //        Tail = newNode;
    //        Count++;
    //        return newNode;
    //    }

    //    //O(1)
    //    public void Remove(MyLinkedListNode<T> node)
    //    {
    //        //기존의 첫 번째 노드의 다음 노드를 첫 번째 노드로 한다.
    //        //한개만 있는 경우도 처리됨.(Head.Next도 null로 되어있기 때문에)
    //        if (Head == node)
    //            Head = Head.Next;

    //        //기존의 마지막 노드의 이전 노드를 마지막 노드로 한다
    //        if (Tail == node)
    //            Tail = Tail.Prev;

    //        if (node.Prev != null)
    //            node.Prev.Next = node.Next;

    //        if (node.Next != null)
    //            node.Next.Prev = node.Prev;

    //        Count--;
    //    }

    //}
    #endregion


    // Mazes for programmers 책 
    internal class Board
    {
        const char CIRCLE = '\u25cf';
        public TileType[,] Tile { get; private set; }//배열
        public int Size { get; private set; }

        public int DestY { get; private set; }
        public int DestX { get; private set; }
        

        Player _player;
        #region 동적 배열 및 연결리스트
        //public List<int> _data2 = new List<int>();              // 동적 배열(c++ 벡터)
        //public MyList<int> _data2 = new MyList<int>();              // 구현한 동적 배열(c++ 벡터)
        //public LinkedList<int> _data3 = new LinkedList<int>();  // 연결 리스트(이중연결리스트)  (c++ 리스트)       중간 삽입/삭제 용이
        //public MyLinkedList<int> _data3 = new MyLinkedList<int>();  // 연결 리스트(이중연결리스트)  (c++ 리스트)       중간 삽입/삭제 용이

        //맵이 사라지거나 생기거나 하지 않은 경우 연결 리스트 효율 감소

        //맵의 사이즈가 커지거나 작아지는 기획이 없는 경우 효율 감소
        #endregion

        public enum TileType
        {
            Empty,
            Wall,
        }
        public void Initialize(int size, Player player)
        {

            //size 는 홀수여야함.
            if (size % 2 == 0)
            {
                return;
            }
            Tile = new TileType[size, size];
            Size = size;

            DestY = Size - 2;
            DestX = Size - 2;

            //plyaer보다 
            GenerateBySideWinder();
            _player = player;

            

            //Mazes for programmers
            //Binary Tree Algorithm
            //GenerateByBinaryTree();
            
            
        }


        void GenerateBySideWinder()
        {
            
            //Mazes for programmers
            //GenerateBySideWinder

            //길을 다 막아버리는 작업(같음)
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    //짝수부분을 벽으로 하여 외각 부분(y = 0, y = size - 1, x = 0, x = size - 1)테두리가 전부 벽이 되도록 만듦.
                    if (x % 2 == 0 || y % 2 == 0)
                        Tile[y, x] = TileType.Wall;    //Red
                    //짝수가 아닌경우
                    else
                        Tile[y, x] = TileType.Empty;   //Green
                }
            }

            
            //랜덤으로 우측 혹은 아래로 길을 뚫는(Empty로 바꿈) 작업
            Random rand = new Random();
            for (int y = 0; y < Size; y++)
            {
                int count = 0;
                for (int x = 0; x < Size; x++)
                {
                    //Empty부분에서 추가로 오른쪽, 혹은 아래로 길을 뚫기 때문에 Wall부분에선 작동하지 않도록 함.
                    //if (Tile[y, x] == TileType.Wall)//x % 2 == 0 || y % 2 == 0)
                    if(x % 2 == 0 || y % 2 == 0)
                        continue;
                    if (x == DestX && y == DestY)
                        continue;
                    //출구쪽(오른쪽 아래 사이드) 외곽의 Empty는 한쪽 방향만 뚫도록 만듦.(출구를 하나로 하기 위해)
                    if (y == Size - 2)
                    {
                        // x+방향으로 Empty 추가
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }
                    if (x == Size - 2)
                    {
                        // y+ 방향으로 Empty 추가
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    //1/2 확률로 하는건 같지만, 연속된 방향으로 뚫은 길 중에서 방향을 바꿀 칸을 정함.
                    //예를 들어, ER-ER-ER-ER-ER 로 연속으로 오른쪽으로 뚫다가 아래로 바뀔 경우
                    //5개의 R 중에 하나를 정해서 아래를 뚫음.
                    if (rand.Next(0, 2) == 0 )
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        count++;

                    }
                    else
                    {
                        int randomIndex = rand.Next(0, count);
                        Tile[y + 1, x - randomIndex * 2] = TileType.Empty;
                        count = 1;
                    }


                }
            }

        }
        void GenerateByBinaryTree()
        {
            //Mazes for programmers
            //Binary Tree Algorithm

            //길을 다 막아버리는 작업(같음)
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    //짝수부분을 벽으로 하여 외각 부분(y = 0, y = size - 1, x = 0, x = size - 1)테두리가 전부 벽이 되도록 만듦.
                    if (x % 2 == 0 || y % 2 == 0)
                        Tile[y, x] = TileType.Wall;    //Red
                    //짝수가 아닌경우
                    else
                        Tile[y, x] = TileType.Empty;   //Green
                }
            }

            //랜덤으로 우측 혹은 아래로 길을 뚫는(Empty로 바꿈) 작업
            Random rand = new Random();
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    //Empty부분에서 추가로 오른쪽, 혹은 아래로 길을 뚫기 때문에 Wall부분에선 작동하지 않도록 함.
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    //출구쪽(오른쪽 아래 사이드) 외곽의 Empty는 한쪽 방향만 뚫도록 만듦.(출구를 하나로 하기 위해)
                    if (y == Size - 2)
                    {
                        //오른쪽으로만 뚫음, 코드 흐름상 먼저 있기 때문에 출구는 오른쪽이 됨.
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }
                    if (x == Size - 2)
                    {
                        //아래쪽으로만 뚫음
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    // 1/2 확률로 
                    if (rand.Next(0, 2) == 0)
                    {
                        //오른쪽으로
                        Tile[y, x + 1] = TileType.Empty;
                    }
                    else
                    {   
                        //아래로
                        Tile[y + 1, x] = TileType.Empty;
                    }
                }
            }
        }

        //
        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    //Plyar 좌표를 갖고와서, 그 좌표랑 현재 y, x가 일치하면 플레이어 전용 색상으로 표시함

                    if (y == _player.PosY && x == _player.PosX)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if (y == DestY && x == DestX)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = GetTileColor(Tile[y, x]);
                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = prevColor;
        }

        ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Wall:
                    return ConsoleColor.Red;
                case TileType.Empty:
                    return ConsoleColor.Green;
                default:
                    return ConsoleColor.Green;
            }
        }
    }
}
