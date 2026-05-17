# 자바로 무엇을 할 수 있을까?
- 자바로 할 수 있는 일
    - 웹 프로그래밍, 안드로이드 앱 개발, 게임 개발, DB 처리, 빅데이터 분산 처리
- 자바로 할 수 없는 일
    - 시스템 프로그래밍, 높은 성능, IOS 앱


# JDK 설치하기
- Java 쓰려면 필수 도구인 JDK(Java Development Kit)를 설치해야 한다
- JDK에는 자바 코드 작성 도구, 컴파일러 등으로 구성


# javac.exe & java.exe
- JRE(Java Runtime Environment)
    - JDK보다 작은 개념으로, 자바가 실행될 수 있는 최소한의 파일들이 설치되어 있는 환경
    - JRE에는 컴파일 도구는 없음
- javac
    - java compiler의 줄임말
    - javac는 java 소스 파일(.java)를 java 클래스 파일(.class)로 바꿔준다
        - 클래스 파일은 JVM이 인식할 수 있는 이진 파일이다


# java 입문
- 클래스명과 파일명이 같은면 그 class 앞에 public을 붙여주는 관례가 있다고 함
- java 프로그램의 시작과 끝은 main 메서드
- 메서드에 static이 붙어 있으면 객체 생성 없이도 실행이 가능하다
- cout 대신 System.out.println을 사용하는 java~
- 가장 바깥쪽은 클래스 블록. 일반적으로는 파일명으로?
- 메서드 블록도 클래스 블록과 마찬가지로 중괄호로 영역을 구분
    - 메서드 블록 안에 명령문은 ;로 마무리!