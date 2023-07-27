# madcamp4_moon_rabbit_unity
2023 여름 몰입캠프 4주차, 달토끼*별쿠키(unity)   
==========
 팀원: [심수연](https://github.com/Shimsuyeon), [김은수](https://github.com/EunsuKim03)   
 개발 환경: Unity, Node.js, MongoDB   
 개발 언어: C#, Javascript, Java   

 게임 개요
 ---------
 *" 달에 사는 달토끼는 행성을 뛰어 다니며    
 별을 모아서 쿠키를 만들어요.   
 쿠키를 많이 팔아서 부자가 될 거예요! "*   

- 로그인
  - EditText로 Id, Pw 입력
  - Id와 Pw는 백엔드 서버를 통해 DB 정보와 비교하며 일치할 경우에 메인 화면으로 이동
  - 일치하지 않는다면, 로그인 실패 메시지를 띄우며 다시 시도
  - 보안을 위해 Pw는 SHA256 Hash를 서버로 보내고 비교함
- 회원 가입
  - EditText로 Id, Pw 입력
  - Id를 서버에 보내며 이미 그 Id가 DB에 존재할 경우, 회원 가입 실패 메시지를 띄움
  - 새로운 Id면 Id와 Pw의 Hash를 DB에 저장하며 로그인 화면으로 돌아감
- 메인 화면
  - ㅇㅇ
- 게임 화면
- 쿠키 상점
- 랭킹 화면
- 쿠키 카페
 
 
