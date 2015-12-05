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
 
 * get()
   .txt 파일로부터 데이터를 flow에 집어넣은 함수
   0. 앞에 필요없는 7줄의 정보는 건너뜀
   1. 한 줄을 받아서 나눔, 7개로 분할됨
   2. 그 중에서 역 번호를 갖고 있는 2개, serperation[1], seperation[3] 과
      number 를 이용해서 출발 역과 도착 역 모두 2호선인지를 확인
      2호선일 경우에는 각각의 인덱스를 받아옴, 2호선이 아닐경우에는 -1을 반환함
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
                            "문래","신도림","대림","구로디지털단지","신대방","신림","봉천"};
            // Station number
            static int[] number ={228, 227, 226, 225, 224, 223,
                           222, 221, 220, 219, 218, 217,
                           216, 215, 214, 213, 212, 211, 210,
                           209, 208, 207, 206, 205,
                           204, 203, 202, 201, 243, 242,
                           241, 240, 239, 238, 247, 248, 249,
                           2519, 244, 245, 250, 246, 237, 236,
                           235, 234, 233, 232, 231, 230, 229};

            public static string lineGet(int x){
                return line[x];
            }
            public static int numberGet(int x){
                return number[x];
            }
        }
        
        // People movement
        public int[,] flow = new int[51, 51];
        // Counter
        int counter;

        // Constructor for reset
        public Data()
        {
            counter = 0;
            for (int x = 0; x < 51; x++)
            {
                for (int y = 0; y < 51; y++)
                    flow[x, y] = 0;
            }
        }

        // Make data
        public void make(string s)
        {
            //when you test this method, PLEASE UPDATE BELOW PATH TO YOUR PATH!!
            //이거 테스트할 때 프로젝트 경로 여러분 걸로 꼭!!! 업데이트 해주세요!! 안그럼 런타임에러남
            //경로 팀원마다 수정 안하게 되도 돌아가게 바꿔놈
            var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            string path = System.IO.Directory.GetParent(projectPath.ToString()).FullName + "/"; //Environment.CurrentDirectory;// @" C:\Users\Jisu\Documents\GitHub\RailoProject\";
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
                        flow[x, y] = Convert.ToInt32(buf2);
                    }
                }
                counter++;
            }
        }

        // Get flow data
        public int get(int x, int y){
            return flow[x, y];
        }

        // Get counter
        public int counterGet(int x){
            return counter;
        }

        // Get Node name
        public string nodeLineGet(int index){
            return Node.lineGet(index);
        }

        // Get Node number
        public int nodeNumberGet(int index){
            return Node.numberGet(index);
        }

        // Matching
        int match(string s)
        {
            int num = Convert.ToInt16(s);
            for (int x = 0; x < 51; x++)
            {
                if (num == Node.numberGet(x))
                    return x;
            }
            return -1;
        }
    }
}
