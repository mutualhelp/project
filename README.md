# 프로젝트 소개
*스마트폰 환경에서 사용자가 터치로 그린 그림을 기존의 저장된 패턴과 비교하여 가장 근접하는 패턴에 대한 이벤트 처리를 위한 유니티 라이브러리

*ex)게임을 예를 들면
원터치방식의 게임이 아닌 사용자가 터치로 그린 패턴을 인식하는 게임을 개발하고자 할때 필요한 라이브러리
	패턴 : 화살표 같이 손으로 그릴수 있는 곡선들을 의미합니다
#개발 내용
입력한 패턴의 표준화
![](https://s3-ap-northeast-1.amazonaws.com/piveapp/p1.jpg)![](https://s3-ap-northeast-1.amazonaws.com/piveapp/p2.jpg)
![](https://s3-ap-northeast-1.amazonaws.com/piveapp/p3.jpg)
#알고리즘 요약
#팀원 역활
#데모

#요약

*최초 실행시 프로젝트 폴더로 부터 미리 저장해둔 Gesture(패턴)를 불러와 전처리 후 List형태로 저장
(사용자가 직접 등록한 Gesture는 다른 경로에 저장이 되어서 불러올때 따로 불러오지만 같은 변수에 저장)

**전처리 알고리즘 : Gesture.cs

	point[] Scale(Point[] points, string gestureName="")

	Gesture의 모든 점들의 x,y좌표를 가로와 세로길이중 최댓값으로 나눠주는 주고,

	x,y좌표중 최솟값을 모든 점들에 대하여 빼기를 하여 Gesture의 위치를 x축과 y축에 접하게 한다.
	
	따라서 다른 Gesture 들의 크기와 위치를 같은 비율로 통일시키는 Method

	point[] TraslateTo(Point[] points, Point p)
	
	Scale Method로 통일시킨 Gesture들을 Centroid(도심)을 (0,0)좌표로 옮겨 기준점으로 삼아
	
	Gesture 위치 값을 재조정

	Point[] Resample(Point[] points, int n)

	사용자가 앞으로 입력할 Gesture의 점의 개수는 매프레임 마다 입력되기 때문에 항상 일정할 수가 없다.

	그렇다면 기존의 저장된 패턴과 새로입력된 패턴이 비교할 데이터 개수가 다르기때문에 비교한 결과를 신뢰할 만한 데이터라고 볼 수 없다.
	
	또한 터치 스크롤 속도에 따라 점의 간격의 분포가 불규칙해 지기 때문에 간격이 일정한 데이터로 처리할 필요가 있다.

	Resample Method는 Gesture를 일정한 간격의 point 집합으로 재추출 하는 과정을 거치는데 이때 재추출은 32개만 한다.

	32개만 하는 이유는 다음 그래프를 보면 이해가 된다. 

![](https://s3-ap-northeast-1.amazonaws.com/piveapp/KakaoTalk_20150625_051638797.png)
	
	8개 추출시 96%, 32개 추출시 98.6%,그리고 32개와 96개 사이로

	추출하였을때는 고작 0.04%의 증가만 있었다. 따라서 효율성 측면에서 32개만 추출해도 정확도 분석에 큰영향을 끼치지 않는다고 판단하였다.

	Gesture의 총 길이를 구하는 PathLength Method를 이용하여 구한 후 31개로 나누어 32개의 구간으로 나눈다. Gesture를 32개 구간으로 나눈 

	점들의 값을 구해야 하는데 그 값은 실제 저장된 점과의 값과 일치 하지는 않지만 특정 점에서의 가장 가까운 두점은 알고 있기에 그 두점을

	m:n으로 내분하여 구간에 위치한 점을 구할 수 있다.
![](https://s3-ap-northeast-1.amazonaws.com/piveapp/a.jpg)


*사용자로 부터 Gesture를 입력받아 전처리 후 List에 저장해둔 Gesture와 비교 후 가장 일치율이 높은 Gesture를 출력해준다.

**비교 알고리즘 : PointCloudRecognizer.cs

	Result Classify(Gesture candidate, Gesture[] trainingSet)

	입력받은 Gesture(candidate)와 저장되어 있던 Gesture를 GreedyCloudMatch Method를 이용해 각각 비교

	float GreedyCloudMatch(Point[] points1, Point[] points2)

	CloudDistance 메소드를 이용하여 두 Gesture를 구성하는 point 사이 거리중 최소값을 리턴해준다.

	float CloudDistance(Point[] points1, Point[] points2, int startIndex)

	point와 point 사이의 거리를 비교하여 가장 가까운 거리의 point 끼리 1대 1 매치 시켜준뒤 총 거리를 가중치를 계산하여 리턴해 준다.


	가중치를 적용하는 이유 : 가장 가까운 점들끼리 비교 후 1대 1 매치 시키는 과정에서 최초로 비교하는 point는 

	모든 point와 비교한 뒤 가장 가까운 point와 매치되지만, 그 다음으로 비교하는 point 들은 점점더 적은 

	point들과 비교하게 되기 때문에 ( i번째 비교하는 point는 n-i개의 point들과 비교후 매치된다)

	startIndex를 조절해 가며 여러번 분석하는 이유 : point 를 비교할 때 1대1로 매치해서 비교하기 떄문에
	
	한번의 비교 만으로는 정확한 비교가 불가능하다.

*일치율이 높은 Gesture의 이름과 일치율을 Result 클래스에 저장한다.
**결과 클래스 : Result.cs
	
	구조체 형태이며 string값의 이름과 float형 변수의 일치율을 변수로 갖는다.
*분석 결과를 출력 한다.
**xml 파일 입출력 작업 및 UI 구성 : Demo.cs

	Start()
	
	xml파일로 저장되어 있는 Gesture들을 읽어 들여 List로 저장한다.

	OnGUI Method에서 사용할 Rect객체 초기화

	Update()

	사용하는 디바이스에 따른 입력방식 설정(PC 및 안드로이드,IOS에 한함)

	매프레임마다 입력을 받아서 Point클래스 형태로 x,y좌표를 List에 저장

	저장된 값을 화면에 출력해 준다.
![](https://s3-ap-northeast-1.amazonaws.com/piveapp/ongui.png)

	OnGUI()

	그래픽 출력을 담당하는 Method

	사각형 구간을 나눠주고 GUI 버튼을 만들어 버튼 이벤트를 발생시킨다.

	Recognize 버튼은 입력된 Gesture를 배열로 변환하여 비교 알고리즘에 넘겨주고 
	
	결과값을 받고 GUI Label로 출력해준다.

	Add 버튼은 사용자가 직접 Gesture를 저장하게 해주며 GestureIO를 사용하여 파일을 쓴다.

	저장 경로는 Application.persistentDataPath로 기기마다 경로가 다른것을 감안하여 사용하였다.

*xml파일을 디렉터리에 읽고 쓴다.
**xml file read&write : GestureIO.cs

	ReadGestureFromXML(string xml)

	xml파일 경로를 입력받아 xml파일만 읽어들여 Gesture에 저장

	ReadGestureFromFile(string fileName)

	디렉터리 경로만을 입력받아 파일을 읽어들여 Gesture에 저장

	ReadGesture(XmlTextReader xmlReader)

	실제적으로 파일을 읽어 들이는 Method로서 Gesture, Stroke, Point를 각각 구분하여

	사용 가능한 데이터로 가공한다.

	WriteGesture(PDollarGestureRecognizer.Point[] points, string gestureName, string fileName)

	사용자가 입력한 데이터를 xml파일로 저장하는 Method로서 StreamWriter를 사용하여 xml파일
	
	형식에 맞게 데이터를 쓰게 하였다.

*그외에 코드들
**Geometry.cs
	SqrEuclideanDistance(Point a, Point b)
	
	두 점 사이의 거리의 차를 제곱하여 더한 값을 반환한다

	EuclideanDistance(Point a, Point b)

	두 점 사이의 거리를 반환한다.
**Point.cs
	float형 x,y변수와 int 형 strokeID를 변수로 가진다.

	Point(float x, float y, int strokeId)	

	생성자로 변수들을 초기화 시켜준다.