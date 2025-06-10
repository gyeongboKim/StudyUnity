using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inflearn_algorithm
{
    class PriorityQueue<T> where T : IComparable<T>    //제네릭 형식 제약조건 :     
    {
        List<T> _heap = new List<T>();

        public void Push(T data)
        {
            // 힙의 맨 끝에 새로운 데이터를 삽입한다.
            _heap.Add(data);

            int now = _heap.Count - 1;
            while (now > 0)
            {
                //대소비교
                int next = (now - 1) / 2;
                //현재 값 보다 다음 값이 크면
                if (_heap[now].CompareTo(_heap[next]) < 0)
                    //실패
                    break;
                //두 값을 교체
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                //검색 위치를 이동
                now = next;
            }
        }
        public T Pop()
        {
            //반환할 데이터를 따로 저장
            T value = _heap[0];

            //마지막 데이터를 루트로 이동
            int lastindex = _heap.Count - 1;
            _heap[0] = _heap[lastindex];
            _heap.RemoveAt(lastindex);
            lastindex--;

            // 역으로 내려가는 대소비교
            int now = 0;
            while (true)
            {
                int left = 2 * now + 1;
                int right = 2 * now + 2;

                int next = now;

                //Left 값이 Right 값보다 크면 왼쪽으로 이동
                //현재 값이 왼쪽 값보다 작으면 왼쪽으로 이동
                if (left <= lastindex && _heap[next].CompareTo(_heap[left]) < 0)    //_heap[next] > _heap[left] ? 1 : -1
                    next = left;
                //오른쪽 값이 현재 값(왼쪽으로 이동한 값 포함) 보다 크면, 오른쪽으로 이동
                if (right <= lastindex && _heap[next].CompareTo(_heap[right]) < 0)
                    next = right;

                //왼쪽 오른 쪽 모드 현재 값보다 작으면 종료
                if (next == now)
                    break;

                //두 값을 교체함
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                //검사 위치 이동
                now = next;
            }

            return value;
        }

        public int Count { get { return _heap.Count(); } }
    }

}
