# LIBRARY
C/C++ 라이브러리 개념 정리


## DLL을 .exe와 같은 디렉터리에 놓는 이유
- C/C++에서 .dll 파일을 .exe와 같은 위치에 두는 이유는 Windows의 DLL 로딩(검색)
  규칙 때문이다

- Windows는 프로그램이 실행될 때 필요한 DLL을 특정 순서대로 찾는데, 
  이걸 Dynamic Link Library 검색 순서라고 보면 된다. 
  
- Windows DLL 검색 순서
```
1. 실행 파일(.exe)이 있는 폴더
2. 시스템 폴더 (예: System32)
3. Windows 폴더
4. 환경 변수 PATH에 등록된 경로들 (Optional)
```

- 리눅스는 빌드 옵션을 통해 지정해준다 
  - 동적 라이브러리 빌드는 경험이 더 필요할듯 (.so)


## pragme once
- 일단 당연하게도 헤더 include는 가급적 소스에서 이루어져야 한다

- 해당 매크로를 통해 헤더가 중복 include 되는 것을 방지할 수 있다


## pragma once를 해도 발생하는 링킹 오류
- 아무리 모든 소스에 대해 해당 매크로를 써도 링킹 오류는 발생 가능

- 자주 경험하는 원인은 여러 소스에서 같은 변수명을 사용하는 경우
  - 이때는 extern, static, inline 등의 테크닉으로 처리 가능
  - 공유되어야 하면 extern, 독립적이라면 static


## extern
- 하나의 변수를 여러 파일에서 공유할 때 사용
  1. 변수 정의는 딱 1번 (여기에는 extern을 붙이지 말 것)
  2. 나머지는 extern 선언

- 일반적인 경우라면 괜찮으나 멀티스레딩 환경에선 문제가 될 수 있다
  - 가급적이면 멀티스레드에선 `extern const` 정도만 쓰자
