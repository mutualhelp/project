# 프로젝트 소개
*스마트폰 환경에서 사용자가 터치로 그린 그림을 기존의 저장된 패턴과 비교하여 가장 근접하는 패턴에 대한 이벤트 처리를 위한 유니티 라이브러리

*ex)게임을 예를 들면
원터치방식의 게임이 아닌 사용자가 터치로 그린 패턴을 인식하는 게임을 개발하고자 할때 필요한 라이브러리
	
	패턴 : 화살표 같이 손으로 그릴수 있는 곡선들을 의미합니다
![](http://aymericlamboley.fr/blog/wp-content/uploads/2014/07/multistrokes.gif)

#개발 내용
  ##입력한 패턴의 표준화
![](https://s3-ap-northeast-1.amazonaws.com/piveapp/p1.jpg)![](https://s3-ap-northeast-1.amazonaws.com/piveapp/p2.jpg)
![](https://s3-ap-northeast-1.amazonaws.com/piveapp/p3.jpg)

  ##유니티로 커스터마이징
![](https://s3-ap-northeast-1.amazonaws.com/piveapp/ongui.png)
	
#알고리즘 요약
	다양한 위치에 다양한 크기, 다양한 속도로 입력되는 하나의 form 으로 변환해주는 패턴을 표준화하고 기존에 저장되어있던 data와 비교한다.

	기존의 data와 비교할 때는 두 Gesture를 이루는 점들을 각각 가까운 점끼리 1대1 매치시킨 뒤 신뢰도 높은 값에 가중치를 주어 매치시킨 점들 사이의 거리를 더한 값을 비교하는 방법을 반복수행 하여 가장 일치율이 높은 Gesture를 추출한다.
	
#팀원 역할
	김상구 : 표준화 및 알고리즘 분석
	박상준 : 알고리즘 분석 및 문서화
	최원준 : UI 및 유니티 커스터마이징
#데모
![](https://s3-ap-northeast-1.amazonaws.com/piveapp/demo1.jpg)
![](https://s3-ap-northeast-1.amazonaws.com/piveapp/demo2.jpg)
![](https://s3-ap-northeast-1.amazonaws.com/piveapp/demo3.jpg)
![](https://s3-ap-northeast-1.amazonaws.com/piveapp/demo4.jpg)
#활용 분야
특정 패턴에 이벤트를 넣어 진행하는 게임 분야
싸인을 패턴으로 저장하여 로그인으로 이용
언어, 정서 발달을 촉진하는 교육용 프로그램에도 
젹용할 수 있으며, 패턴을 이용할 수 있는 타 서비스의 기반으로 이용할 수 있습니다.

#장점

C#을 기반으로 구성된 라이브러리이지만, 안드로이드, IOS, 플레이스테이션, 웹, PC환경으로
뱉어 낼 수 있습니다.
플랫폼이라는 클래스를 이용하여 현재 플랫폼에 대한 정보를 가져오고 플랫폼 별로 설정을 할 수 있습니다.

