# madcamp4_moon_rabbit_unity
2023 여름 몰입캠프 4주차, 달토끼*별쿠키(unity)   
==========
 팀원: [심수연](https://github.com/Shimsuyeon), [김은수](https://github.com/EunsuKim03)   
 개발 환경: Unity, Node.js, MongoDB   
 개발 언어: C#, Javascript, Java   

 게임 개요
 ---------
<img src="https://github.com/Shimsuyeon/madcamp4_moon_rabbit_unity/assets/109589438/e51edb7b-f559-41d2-917b-f3c2eae53ef4" width="150" height="150">
 
 *" 달에 사는 달토끼는 행성을 뛰어 다니며    
 별을 모아서 쿠키를 만들어요.   
 쿠키를 많이 팔아서 부자가 될 거예요! "*   

- 로그인
  - EditText로 Id, Pw 입력
  - Id와 Pw는 백엔드 서버를 통해 DB 정보와 비교하며 일치할 경우에 메인 화면으로 이동
  - 일치하지 않는다면, 로그인 실패 메시지를 띄우며 다시 시도
  - 보안을 위해 Pw는 SHA256 Hash를 서버로 보내고 비교함
  <p align="center"><img src="https://github.com/Shimsuyeon/madcamp4_moon_rabbit_unity/assets/109589438/ebb983b1-0cdd-4b2f-8c03-fd5bb6fb3770" width="300" height="600">
- 회원 가입
  - EditText로 Id, Pw 입력
  - Id를 서버에 보내며 이미 그 Id가 DB에 존재할 경우, 회원 가입 실패 메시지를 띄움
  - 새로운 Id면 Id와 Pw의 Hash를 DB에 저장하며 로그인 화면으로 돌아감
  <p align="center"><img src="https://github.com/Shimsuyeon/madcamp4_moon_rabbit_unity/assets/109589438/37d9986c-e6a0-47e2-a1cd-204f23a8657f" width="300" height="600">
- 메인 화면
  - 각 화면으로 이동하는 버튼들
  <p align="center"><img src="https://github.com/Shimsuyeon/madcamp4_moon_rabbit_unity/assets/109589438/987b6e46-a93f-4668-b1db-7e8df67a9f77" width="300" height="600">
- 게임 화면
  - 핸드폰 기울기(가속도 센서)를 이용하여 토끼를 조작   
  - 아래로 기울일 시, 게이지가 충전되며 게이지가 충전된 상태에서 위로 기울이면 토끼가 게이지에 비례하는 힘으로 점프
  - 토끼는 행성에 착지해야 하며, 착지하지 못하면 떨어지고 게임 오버 UI가 뜨게 됨
  - 오브젝트   
    > - 행성: 토끼가 밟을 수 있는 곳, 밟으면 +5점
    > - 별쿠키: 토끼가 먹어서(부딪혀서) 모아야 함, 먹으면 +10점
    > - 태양: 행성 중 일정 확률로 나타나고 밟으면 3 ~ 6 행성 거리로 슈퍼 점프함, +100점
    > - 블랙홀: 별쿠키가 일정 확률로 블랙홀로 변하며 부딪히면 게임 오버
  - 점수에 비례하여 토끼의 이동 속도가 증가함
  - 일시정지 UI: 좌측 상단 일시정지 버튼 누를 시 게임을 잠시 멈춤
  - 게임오버 UI: 게임 오버시에 나타나며 그 게임의 점수와 획득한 별쿠키를 종류별로 표시
  <p align="center"><img src="https://github.com/Shimsuyeon/madcamp4_moon_rabbit_unity/assets/109589438/01425bba-596c-4ed0-85c7-e10f3aa4a524" width="300" height="600">
  <p align="center"><img src="https://github.com/Shimsuyeon/madcamp4_moon_rabbit_unity/assets/109589438/000f206b-e437-4878-9b4a-6c8035635ce5" width="300" height="600">
- 쿠키 상점
  - 쿠키는 종류가 6개가 있으며 각각 필요한 별쿠키의 양이 다름
  - 각 쿠키 터치 시, 쿠키의 이름, 설명과 구매 화면이 뜨며 하단 버튼을 통해 구매 가능, 별쿠키가 부족할 시에는 살 수 없음
  - 쿠키 구매시에 상점으로 돌아오며 해당 쿠키의 그림이 활성화 됨
  - 이미 보유한 쿠키 터치시에 쿠키 설명을 볼 수 있고 판매 버튼이 나옴
  - 쿠키를 판매하여 별을 얻을 수 있음
    <p align="center"><img src="https://github.com/Shimsuyeon/madcamp4_moon_rabbit_unity/assets/109589438/a13b4670-ddf0-41da-ae2b-4119069f12b7" width="300" height="600">
- 랭킹 화면
  - DB에서 모든 유저의 정보를 불러오며, 점수로 정렬하여 순위를 표시
  - 1, 2, 3등은 글자색을 다르게 함
  - 알 수 없는 이유로 스크롤해도 손을 때면 다시 원래대로 돌아옴
  <p align="center"><img src="https://github.com/Shimsuyeon/madcamp4_moon_rabbit_unity/assets/109589438/a272b3dd-520e-44b0-9936-0391f0cb732a" width="300" height="600">
- 쿠키 카페
  - 토끼의 카페를 3D 그래픽으로 보여줌
  - 토끼는 쿠키를 판매하여 얻은 별로 가구를 구매하여 카페를 꾸밀 수 있음
  - 각 가구는 에셋 기본 기능으로 터치하면 애니메이션이나 상호 작용을 보여줌
<p align="center"><img src="https://github.com/Shimsuyeon/madcamp4_moon_rabbit_unity/assets/109589438/a5d84dcd-69dc-4883-b726-183b1cdb4100" width="300" height="600">
 
 
