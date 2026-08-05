# DB Source
DB 프로그램의 소스코드 읽으면서 CPP 개념 채워넣기


# 명시적 디폴트 생성자 생성 및 삭제
디폴트 생성자가 있거나 없다는 것을 알리기 위해 default & delete 키워드 사용

```cpp
class sample{
public:
    sample() = default; // 디폴트 생성자 존재함
    sample() = delete; // 디폴트 생성자 존재 X
}
```


# 멤버 함수 const 키워드 (상수 멤버 함수)
- 멤버 함수에 const 키워드가 뒤에 붙으면, 그 함수는 멤버 변수 값을 변경 못함!
- 상수 객체는 상수 멤버 함수만 호출할 수 있다
    - 상수 객체는 값이 바뀌지 않기 때문에, 값을 바꾸는 함수 자체에 대한 호출을 막는 것
- const 키워드의 보다 적절한 사용에 대해서는 이펙티브의 항목 3과 20을 볼 것


# CRTP(Curiously Recurring Template Pattern)
- Purpose: 부모가 자식 타입을 컴파일 타입에 알게 만들기
    - 가상함수 없이 다형성 구현..?
    - 즉, 부모 클래스에서 자식 함수를 virtual 없이 호출할 수 있음
