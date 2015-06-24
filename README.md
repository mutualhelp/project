# project
*패턴인식 라이브러리

*기존의 패턴인식 라이브러리를 unity 개발자가 사용하기 편리하도록 수정

#요약

*최초 실행시 DataBase로 부터 미리 저장해둔 Gesture(패턴)를 불러와 전처리 후 List형태로 저장

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

	32개만 하는 이유는 다음 그래프를 보면 이해가 된다. ![](https://s3-ap-northeast-1.amazonaws.com/piveapp/KakaoTalk_20150625_051638797.png)
	
	8개 추출시 96%, 32개 추출시 98.6%,그리고 32개와 96개 사이로

	추출하였을때는 고작 0.04%의 증가만 있었다. 따라서 효율성 측면에서 32개만 추출해도 정확도 분석에 큰영향을 끼치지 않는다고 판단하였다.


*사용자로 부터 Gesture를 입력받아 전처리 후 List에 저장해둔 Gesture와 비교 후 가장 일치율이 높은 Gesture를 출력해준다.

**비교 알고리즘 : PointCloudRecognizer.cs

	Result Classify(Gesture candidate, Gesture[] trainingSet)

	입력받은 Gesture(candidate)와 저장되어 있던 Gesture를 GreedyCloudMatch Method를 이용해 각각 비교

	float GreedyCloudMatch(Point[] points1, Point[] points2)

	CloudDistance 메소드를 이용하여 두 Gesture를 구성하는 point 사이 거리중 최소값을 리턴해준다.

	float CloudDistance(Point[] points1, Point[] points2, int startIndex)

	point와 point 사이의 거리를 비교하여 가장 가까운 거리의 point 끼리 1대 1 매치 시켜준뒤 총 거리를 가중치를 계산하여 리턴해 준다.


	가중치를 적용하는 이유 : 가장 가까운 점들끼리 비교 후 1대 1 매치 시키는 과정에서 최초로 비교하는 point는 모든 point와 비교한 뒤 가장 가까운 point와 매치되지만, 그 다음으로 비교하는 point 들은 점점더 적은 point들과 비교하게 되기 때문에 ( i번째 비교하는 point는 n-i개의 point들과 비교후 매치된다)

	startIndex를 조절해 가며 여러번 분석하는 이유 : point 를 비교할 때 1대1로 매치해서 비교하기 떄문에 한번의 비교 만으로는 정확한 비교가 불가능하다.



$P Point-Cloud Recognizer

=========================

[Original article](http://depts.washington.edu/aimgroup/proj/dollar/pdollar.html). [Unity Web demo](http://aymericlamboley.fr/blog/wp-content/uploads/2014/07/index.html).


This is an adaptation of the original C# code for working with Unity.


In the demo, only one point-cloud template is loaded for each of the 16 gesture types. You can add additional templates as you wish, and even define your own custom gesture templates.


![](https://s3-ap-northeast-1.amazonaws.com/piveapp/KakaoTalk_20150625_051638797.png)


About

-----

The [$P](http://depts.washington.edu/aimgroup/proj/dollar/pdollar.html) Point-Cloud Recognizer is a 2-D gesture recognizer designed for rapid prototyping of gesture-based user interfaces. In machine learning terms, $P is an instance-based nearest-neighbor classifier with a Euclidean scoring function, i.e., a geometric template matcher. $P is the latest in the dollar family of recognizers that includes [$1](http://depts.washington.edu/aimgroup/proj/dollar/index.html) for unistrokes and [$N](http://depts.washington.edu/aimgroup/proj/dollar/ndollar.html) for multistrokes. Although about half of $P's code is from $1, unlike both $1 and $N, $P does not represent gestures as ordered series of points (i.e., strokes), but as unordered point-clouds. By representing gestures as point-clouds, $P can handle both unistrokes and multistrokes equivalently and without the combinatoric overhead of $N. When comparing two point-clouds, $P solves the classic [assignment problem](http://en.wikipedia.org/wiki/Assignment_problem) between two bipartite graphs using an approximation of th
