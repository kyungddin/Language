# C-Study
C++ 공부 기록용 Repository


## To Do List
1. C++ Concepts
2. Library Concept Review
3. Effective C++
4. More Effective C++
5. Effective Modern C++
6. Design Pattern


## Memo
- noexcept
    - 함수의 예외사항을 고려하지 않음을 명시하는 것
    - 만약 예외상황 발생시 `std::terminate();`으로 `exit(1);` 이 발생한다

- 소스 extern vs 헤더 extern
    - 소스 extern: 그저 다른 파일의 변수를 참조할 때 사용
    - 헤더 extern: 공유 전역 변수 선언 시에 사용 (general purpose)

- Visual Studio 디렉터리 설정 (\ vs /)
    - 원래 Windows에선 \가 디렉터리 경로에 쓰인다 (mac/linux에선 당연히 /)
    - 그러나, 요즘엔 Windows API 선에서 /를 자동으로 \로 바꿔준다
    - \는 \n과 같이 쓰이는 용도가 있으니 /를 사용하기를 권장

- 더 메꿔야할 문법
    - Virtual
    - Friend
    - Template
    - Macro

- 매크로 함수 Review
    ```cpp
    #define FUNC(a,b) a+b // #define 이후 함수명(인자) 반환값
    ```

    