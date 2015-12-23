using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Get, Set 는 생략

 * 한국어
 * class Node
   역을 뜻하는 노드 클래스를 만들어서 line과 number을 관리함
   바뀌지 않고, 어디서나 읽기만 하니 static으로 선언함

 * string[] line
   역 이름들 모아놓은 스트링 <= 소영이가 요구함
 
 * int[] number
   제공 받은 엑셀 파일에 정류장 이름에 이것저것 숫자들이 붙어있어서
   2호선에 있는 역인지 구분하기 어려워서 사용함
   각 역마다 고유 역번호가 있어서 구분하기 쉬움
 
 * flow
   사람 이동양을 찾은 데이터들
   실질적으로 사용하게될 배열
 
 * counter
   .txt 파일 보면 아는데 처음 7줄은 필요없는 내용들
   뛰어서 다른 데이터들 받아야해서 카운팅 하는데 사용
   데이터 얼마나 되는지 측정하는데에도 유용할듯(?)
 
        +현경 
    내가 쓴 파일 대로 하면 counter ㄴㄴ 그래서 카운더는 굳이 넣지 않았음!! 
    
    + wooku data : 운행년월   출발역코드   출발역명   도착역코드   도착역명   인원   역간거리(km)
    + hyunkyung data  사용년월   승차역   하차역   사용자   이용인원
    + 

 * get()
 hk. 
    0.사용년월   승차역   하차역   사용자   이용인원
    1.한 줄을 받아서 나눔, 5개로 분할 됨 - > 7개로 바꾸어줌 
    2. 최대한 우준혁하고 비슷하게 할려고 함 그래서 코드를 받아와서 역명을 알게 함 
 3. 역간거리는 Null로 초기화함 
 4. 대상은 거리 다음에 있음(역간거리다음에)
 5. *Flow에 덮는다 Make_New를 사용하면 최신 데이터(날짜가 최신껄로)업데이트됨 만약 내 데이터가 없으면 우준데이터가 살아있고 있으면
 모.. 우준처럼 죽음 ㅎ 
 
   .txt 파일로부터 데이터를 flow에 집어넣은 함수
   0. 앞에 필요없는 7줄의 정보는 건너뜀
   1. 한 줄을 받아서 나눔, 7개로 분할됨 
   2. 그 중에서 역 번호를 갖고 있는 2개, serperation[1], seperation[3] 과
      number 를 이용해서 출발 역과 도착 역 모두 2호선인지를 확인
      2호선일 경우에는 각각의 인덱스를 받아옴, 2호선이 아닐경우에는 -1을 반환함
      //다 2호
   3. 둘 다 2호선이면 이동인구에 해당하는 스트링에
      필요 없는 데이터들을 지움, 예를들자면 단위를 표시하기 위해 사용한 , 라던가 강조하려고 쓴 "
   4. 데이터를 int형으로 변환한뒤에 데이터를 flow 매트릭스에 집어 넣음, 좌표의 경우 ( seperation[1], seperation[3] ) 위치에
 
 * English
 * 
 * string[] line
   Station name will be used for next part
 
 * int[] number
   Each of stations have their own station number.
   It is very difficult to make data set using statino name.
   So I make another data line. It is station number.
 
 * flow
   Collect movement.
 
 * counter
   There are 7 useless lines.
   I need to skip that lines, so I use counter

 * make()
   get a line and find data which we need, and push that data in flow matrix
   1. Read 1 line from .txt
   2. Match station number
      If it is matched return index, else return -1;
   3. Split string
   4. remove useless character ex) ", blank
   5. Push data in matrix
 */

namespace RailroProject
{
    public class Data
    {
        // Station name
        public static class Node
        {
            static string[] line = {"서울대입구", "낙성대", "사당","방배","서초", "교대",
                            "강남","역삼","선릉","삼성","종합운동장","신천",
                            "잠실","잠실나루","강변","구의","건대입구","성수","뚝섬",
                            "한양대", "왕십리", "상왕십리","신당","동대문역사문화공원",
                            "을지로4가","을지로3가","을지로입구","시청","충정로","아현",
                            "이대","신촌","홍대입구","합정","도림천","양천구청","신정네거리",
                            "까치산","용답","신답","용두","신설동","당산","영등포구청",
                            "문래","신도림","대림","구로디지털단지","신대방","신림","봉천" //여기까지 51개, 환승역X
                         // "강남","건대입구","교대","당산","대림","동대문역사공원","동대문역사공원",
                         // "사당","선릉","시청","신당","신도림","신설동","영등포구청",
                         // "왕십리","을지로3가","을지로4가","잠실","충정로","합정","홍대입구", "홍대입구"
                            };
            // Station number
            static int[] number ={228, 227, 226, 225, 224, 223,
                           222, 221, 220, 219, 218, 217,
                           216, 215, 214, 213, 212, 211, 210,
                           209, 208, 207, 206, 205,
                           204, 203, 202, 201, 243, 242,
                           241, 240, 239, 238, 247, 248, 249,
                           2519, 244, 245, 250, 246, 237, 236,
                           235, 234, 233, 232, 231, 230, 229, //여기까지 51개, 환승역X
                           4307, 2729, 330, 4113, 2746, 422, 2537,
                           433, 1023, 151, 2636, 1007, 156, 2524,
                           1013, 320, 2536, 2815, 2532, 2623, 1264, 4203
                           };

            public static int getnumberIndex(string s)
            {

                int i;
                for (i = 0; i < 73 ; i++)
                {
                    if (Node.number[i] == Int32.Parse(s)) break;
                }

                if (i == Node.size()) return -1;
                if (i >= 51)
                {
                    switch (i)
                    {
                        case 51: i = 6; break;
                        case 52: i = 16; break;
                        case 53: i = 5; break;
                        case 54: i = 42; break;
                        case 55: i = 46; break;
                        case 56: i = 23; break;
                        case 57: i = 23; break;
                        case 58: i = 2; break;
                        case 59: i = 8; break;
                        case 60: i = 27; break;
                        case 61: i = 22; break;
                        case 62: i = 45; break;
                        case 63: i = 41; break;
                        case 64: i = 43; break;
                        case 65: i = 20; break;
                        case 66: i = 25; break;
                        case 67: i = 24; break;
                        case 68: i = 12; break;
                        case 69: i = 28; break;
                        case 70: i = 33; break;
                        case 71: i = 32; break;
                        case 72: i = 32; break;
                    }
                }
                //int debug = Array.BinarySearch(Node.number, Int32.Parse(s));


                return i;
            }

            public static int size()
            {
                return line.GetLength(0);
            }

            public static string lineGet(int x)
            {
                return line[x];
            }
            public static int numberGet(int x)
            {
                return number[x];
            }
        }

        // People movement
        public int[,] flow = new int[Node.size(), Node.size()];
        // Counter
        int counter;

        // Constructor for reset
        public Data()
        {
            counter = 0;
            for (int x = 0; x < Node.size(); x++)
            {
                for (int y = 0; y < Node.size(); y++)
                    flow[x, y] = 0;
            }
        }

        //copy constructor
        public Data(Data data)
        {
            for (int i = 0; i < Node.size(); i++)
            {
                for (int j = 0; j < Node.size(); j++)
                {
                    this.flow[i, j] = data.flow[i, j];
                }
            }
            this.counter = data.counter;
        }

        // Make data
        public void make(string s)
        {
            //경로 팀원마다 수정 안하게 되도 돌아가게 바꿔놈
            var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            string path = System.IO.Directory.GetParent(projectPath.ToString()).FullName + "/";

            string buf;
            path = path + s;
            System.IO.StreamReader file = new System.IO.StreamReader(path, Encoding.Default);
            while ((buf = file.ReadLine()) != null)
            {
                if (counter > 7)
                {
                    string[] seperation = buf.Split('\t');
                    int x = match(seperation[1]), y = match(seperation[3]);
                    if (x != -1 && y != -1)
                    {
                        string buf2 = seperation[5].Replace("\"", "");
                        buf2 = buf2.Replace(" ", "");
                        buf2 = buf2.Replace(",", "");
                        buf2 = buf2.Replace("-", "0");
                        if (x < 51 && y < 51)
                            flow[x, y] = Convert.ToInt32(buf2);
                        else
                        {
                            x = getHStation(x);
                            y = getHStation(y);
                            flow[x, y] += Convert.ToInt32(buf2);

                        }
                    }
                }
                counter++;
            }
            file.Close();
        }
        //hyunkyung
        public void make_new(string s)
        {
            //경로 팀원마다 수정 안하게 되도 돌아가게 바꿔놈
            var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            string path = System.IO.Directory.GetParent(projectPath.ToString()).FullName + "/";

            string buf;
            path = path + s;
            System.IO.StreamReader file = new System.IO.StreamReader(path, Encoding.Default);
            while ((buf = file.ReadLine()) != null)
            {
                if (counter > 0)
                {
                    string[] buf2 = new string[8];
                    string[] seperation = buf.Split('\t');
                    int x = match(seperation[1]), y = match(seperation[3]);
                    buf2[0] = seperation[0];
                    buf2[1] = seperation[1];
                    buf2[2] = Node.lineGet(Node.getnumberIndex(seperation[2]));
                    buf2[3] = seperation[3];
                    buf2[4] = Node.lineGet(Node.getnumberIndex(seperation[4]));
                    buf2[5] = seperation[6];
                    buf2[6] = null;
                    buf2[7] = seperation[5];

                }
                counter++;
            }
            file.Close();
        }

        // Get flow data
        public int get(int x, int y)
        {
            return flow[x, y];
        }

        // Get counter
        public int counterGet(int x)
        {
            return counter;
        }

        // Get Node name
        public string nodeLineGet(int index)
        {
            return Node.lineGet(index);
        }

        // Get Node number
        public int nodeNumberGet(int index)
        {
            return Node.numberGet(index);
        }

        // Matching
        int match(string s)
        {
            int x;
            int num = Convert.ToInt16(s);
            for (x = 0; x < 73; x++)
            {
                if (num == Node.numberGet(x))
                    return x;
          
            }
            return -1;
        }
        public int getHStation(int x)
        {
            //환승역 위치의 계산
            if (x >= 51)
            {
                switch (x)
                {
                    case 51: x = 6; break;
                    case 52: x = 16; break;
                    case 53: x = 5; break;
                    case 54: x = 42; break;
                    case 55: x = 46; break;
                    case 56: x = 23; break;
                    case 57: x = 23; break;
                    case 58: x = 2; break;
                    case 59: x = 8; break;
                    case 60: x = 27; break;
                    case 61: x = 22; break;
                    case 62: x = 45; break;
                    case 63: x = 41; break;
                    case 64: x = 43; break;
                    case 65: x = 20; break;
                    case 66: x = 25; break;
                    case 67: x = 24; break;
                    case 68: x = 12; break;
                    case 69: x = 28; break;
                    case 70: x = 33; break;
                    case 71: x = 32; break;
                    case 72: x = 32; break;
                }
            }

            return x;
        }
        //converts digraph flow to undirected graph
        public void getUndirected()
        {
            int[,] newFlow = new int[Node.size(), Node.size()];
            for (int x = 0; x < Node.size(); x++)
            {
                for (int y = 0; y < Node.size(); y++)
                {
                    if (y >= x)
                    {   //newFlow[x,y] is the average of flow[x,y] and flow[y,x]
                        newFlow[y,x] = newFlow[x, y] = (flow[x, y] + flow[y, x]) / 2;
                    }
                }
            }
            this.flow = newFlow;
        }

    }
}