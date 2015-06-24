# project
*패턴인식 라이브러리

*기존의 패턴인식 라이브러리를 unity 개발자가 사용하기 편리하도록 수정

#요약

*최초 실행시 DataBase로 부터 미리 저장해둔 Gesture(패턴)를 불러와 전처리 후 List형태로 저장

**전처리 알고리즘 : Gesture.cs

	point[] Scale(Point[] points, string gestureName="")

	각기 다른 Gesture 들의 크기와 위치를 통일시키는 Method

	point[] TraslateTo(Point[] points, Point p)

	Centroid(도심)을 기준점으로 삼아 Gesture 위치를 재조정

	Point[] Resample(Point[] points, int n)

	Gesture를 일정한 갯수, 일정한 간격의 point 집합으로 재추출 하는 과정

	재추출 하는 이유 : 터치입력 속도에 따라 점의 간격과 분포가 불규칙해 지는것을 점과 점 사이의 간격이 동일하게 바꿔주기 위하여.


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
