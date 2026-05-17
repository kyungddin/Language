# CPP Concept
CPP 개념 공부하는 곳


---


# 씹어먹는 CPP


## cin vs scanf
- scanf와 달리 cin이 주소값 전달이 필요없는 이유
    - cin이 레퍼런스를 사용하기 때문!


## const 레퍼런스
- 상수 리터럴을 일반 레퍼런스가 참조하는 것은 불가능하다
    ```cpp
    int &ref = 4;           // (x)
    const int &ret = 4;     // (o)
    ```

- 원래는 함수의 return 값도 임시값이므로 레퍼런스가 불가하지만
    - const 레퍼런스는 이 수명을 늘려서 가능하게 한다!


## Illegal References
- 레퍼런스는 메모리 상에서 공간을 차지하므로 이에 위배되는 레퍼런스는 불가!
    - 레퍼런스의 레퍼런스
    - 레퍼런스의 배열
    - 레퍼런스의 포인터


## 리터럴(Literal)
- 개념: 소스 코드 상에서 고정된 값을 가지는 것
    - 큰 따옴표로 묶인 것은 문자열 리터럴

- 상수와의 차이
    - 상수는 값이 안 바뀌는 "변수 또는 식별자"
    
    ```cpp
    #define PI 3.14
    const int a = 10;
    ```

- 리터럴은 코드 영역에 정의되며, 읽기만 가능하다


## 리터럴과 레퍼런스
- 레퍼런스가 상수를 참조하는 것은 불가능하다

    ```cpp
    int &ref = 4; // Compile Error!
    ```

- 대신에 상수 참조자는 가능하다

    ```cpp
    const int &ref = 4;
    ```


## Function vs Procedure
- Fuction: 값을 반환하는 함수 (값 중심)
- Procedure: 값을 반환하지 않는 함수 (동작 중심)


## 캡슐화의 장점
- 물론 캡슐화의 원래의 목적은 객체가 훼손되는 것을 방지하기 위함이다

- 그러나 보다 와닿는 현실적인 장점은 바로 디버깅의 용이 때문인 거 같다
    - 만약, 클래스 쪽에서 문제가 터지면 Setter만 체크하면 되니까


## 오버로딩에서의 Ambiguous
- C++ 컴파일러 오버로딩 3단계
    1. 자신과 타입이 정확히 일치하는 함수를 찾는다
    2. 형변환을 통해 일치하는 함수를 찾는다
        ```markdown
        
        1. char, unsigned char, short -> int
        2. unsigned short -> int or unsigned int (chose by size of int)
        3. float -> double
        4. enum -> int

        ```
    3. 그래도 일치하지 않으면 좀 더 포괄적인 형변환이 발생한다
        ```markdown
        
        1. 임의의 숫자는 다른 숫자 타입으로 변환 (ex: float -> int)
        2. enum도 숫자 타입으로 변환
        3. null?
        4. 포인터는 void 포인터로 변환

        ```
    4. 유저 정의된 타입을 찾는다

- 이 때, 같은 단계에서 두 개 이상 일치하면 오류 발생 (Ambiguous Error!)
    ```cpp

    #include <iostream>
    using namespace std;

    void func(long x)
    {
        cout << x;
    }

    void func(double x)
    {
        cout << x;
    }

    int main()
    {
        int a = 10;
        func(a);

        return 0;
    }

    ```


## 모든 기능은 필요에 의해 만들어진다
- 생성자: 일반적으로 초기화를 위함
- 소멸자: 일반적으로 객체 뒷처리를 하기 위함


## 복사 생성자 정의하기
- 복사 생성자 오버로딩 규칙
    ```cpp
    T(const T& a);
    ```
    - 코딩 스타일: 클래스에 &가 붙는다

- const를 인자 타입으로 반드시 붙일 필요는 없다
    - 단, const 타입 객체까지 복사하려면 반드시 const를 붙여줘야 함


## 복사 생성자가 호출되는 경우
- 복사 생성자가 호출되는 2가지 케이스

    1. 인자로 객체를 넣는 경우
        ```cpp
        Photon_Cannon pc1(pc2);
        ```

    2. 생성과 "동시에 대입" 하는 경우
        ```cpp
        Photon_Cannon pc1 = pc2; // 컴파일러는 1번과 같게 판단한다
        ```

- 복사 생성자가 호출되지 않는 케이스
    ```cpp
    Photon_Cannon pc1;
    pc1 = pc2; // 이건 그냥 대입이다!
    ```


## 포인터에서 const의 용법 (상수 포인터 vs 포인터 상수)
- 상수 포인터
    - 상수를 가리키는 포인터
    ```cpp
    const int *p; // const int를 가리키는 p!
    ```
- 포인터 상수
    - 주소가 바뀌지 않는 포인터
    ```cpp
    int const *p; // p가 const하다!
    ```


## 함수 매개변수의 const
- 함수가 매개변수의 값을 변화시키는 일이 없다면 const를 붙이는 것을 습관처럼!
- 함수 정의를 작성하고 난 후, 인자가 값이 변하는 일이 없다면 과감하게 const를 붙이자!
- const는 디버깅 시간을 줄여준다 (원인 특정이 용이하니)


## 디폴트 복사 생성자의 한계
- 클래스에서 포인터를 다룬다면 복사 생성자를 오버로딩해주는 습관을 가지자!


## C언어처럼 문자열을 다루지 말자
- C++에서 char*를 이용한 문자열은 매우 비추
    - 버그가 발생할 가능성이 높다 (메모리 누수, 오버플로우 등)
    - 사용하기 불편하다 (strlen 등)

- 아주 편한 <string> STL을 써주자


## 초기화 리스트 (Initializer List)
- 생성자 호출과 동시에 멤버 변수들을 초기화
    ```cpp
    Marine::Marine(int x, int y)
        : coord_x(x), coord_y(y), hp(50) 
    {}
    ```

- 인자 이름과, 멤버 변수 이름이 같아도 정상적으로 컴파일 된다!
    - 단, 일반적으로 멤버 변수는 **m_** 을 붙이니, 그럴 일은 없을 거 같다

- 초기화 리스트가 중요한 이유 1
    - 초기화 리스트는 생성과 초기화를 동시에 한다
    - 따라서 더 직접적이고 효율적인 초기화이다

- 초기화 리스트가 중요한 이유 2
    - 클래스 내부에 레퍼런스 변수나 상수를 넣고 싶으면, 반드시 초기화 리스트!
    - 객체가 만들어지는 순간 초기화되어야 하는 것들이니

- 딱히 기본 생성자가 없는 클래스에도 초기화 리스트를 활용해보자


## static
- C언어에서의 Static
    1. 지역 변수: 함수가 끝나도 프로그램 종료시까지 존재
    2. 전역 변수: 다른 파일에서 extern으로 접근 불가
    3. static 함수: 다른 파일에서 extern으로 접근 불가

- C++ Class Static (Class 멤버 Static)
    - 모든 객체가 공유하는 변수 및 함수
    - static 멤버 변수 정의
        1. static 멤버 변수는 반드시 클래스 밖에서 정의를 해야 쓸 수 있음
            ```cpp
            int A::count = 0;
            ```
        2. 그러나, static const 같은 경우는 내부에서 초기화 가능
            - 참고로 모던 C++에서부터는 멤버 변수도 클래스 안에서 초기화 가능
    - static 사용하기
        ```cpp
        A::func(); // 이런식으로 범위 지정 연산자(::)를 써줘야 한다
        ```

- 나중에 디자인 패턴인 싱글톤 패턴이나, 이펙티브 시리즈 보면서 Static 체화


## this 포인터와 reference 리턴
- `return *this;` 를 쓰는 상황에서 함수 return 타입에 따라 동작이 달라짐

- 레퍼런스를 리턴하는 함수
    ```cpp
    Marine& Marine::attack(int damage)
    {
        hp -= damage;
        if(hp <= 0) is_dead = true;

        return *this;
    }
    ```
    - 이런 식으로 레퍼런스와 `return *this;` 를 조합해 함수 체이닝 가능
    ```cpp
    marine.attack(10).attack(20).attack(30);
    ```

- 클래스에서 레퍼런스 대신 값을 리턴하면? (매우 중요)
    - 리턴 과정에서 임시 객체를 생성하는 문제가 발생한다
    - 또한 이러한 임시 객체는 레퍼런스 변수에 넣어줄 수 없다
    - 또한, 임시 객체가 생성된 후 *this에 복사되므로 복사 생성자 역시 호출
    - 자세한 것은 p.133 참고


## 좌측값/우측값
- 좌측값: 이름이 있고, 주소가 있는 값 (어디 저장된 값)
    - 주소 있음 (&a 가능)
    - 변수 자체
    - 대입 연산자 왼쪽에 올 수 있음

- 우측값: 일시적인 값 (계산 결과로 나온 값)
    - 임시 값
    - 이름 없음
    - 주소 못 가져오는 겨우 많음

- 모던 C++ 공부할 때 주의해서 보기


## const 함수 (상수 함수)
- 변수들의 값을 읽기만 하는 함수
    ```cpp
    int attack() const; // 데미지를 리턴만 하는 함수
    ```

- 상수 함수는 읽기만이 수행되므로, 상수 함수 안에서 다른 함수를 호출할 경우
그 함수 역시 상수함수여야 한다!

- 역시 getter 함수에서 쓰는 것이 유용

- 오버로딩 테크닉
    ```cpp
    class A {
    int x;
    public:
        int& get() { return x; } // 쓰기용
        const int& get() const { return x; } // 읽기용
    };
    ```


## mutable 키워드
- 멤버 변수에 mutable을 붙이면 const 함수 안에서도 수정이 허용된다
- 보통 잘 안 쓰며, 캐시에 활용된다
    ```cpp
    class A {
        int value;
        mutable int cache;
        mutable bool cached = false;

    public:
        int compute() const {
            if (!cached) {
                cache = value * 2;
                cached = true;
            }
            return cache;
        }
    }; 
    ```
    - 이처럼 논리적 상태인 `value * 2` 라는 값은 그대로
    - 다만 이를 매번 계산하는 것은 낭비이니 내부적으로 cache 라는 보조값을 이용


## explicit 키워드
- 명시적 변환을 강제할 때 사용한다
    ```cpp
    class MyString{
    public:
        explicit MyString();
    };
    ```
    - 이 경우에 MyString 객체는 암시적 변환으로 인한 생성 자체가 막힌다!

- 암시적 변환은 =, 즉 대입 연산자에 의해 발생한다
    - 클래스에서 `MyString s = "abc";` 와 같이 대입 연산자로 복사 생성이 일어난다
    - 그러나, 생성자에 explicit 키워드를 적어 놓으면 이러한 복사 생성 자체가 막힌다! (암시적 변환 가능성 자체를 차단)


## 대입 연산자 함수 (연산자 오버로딩)
- 연산자 오버로딩의 대표적인 함수
    ```cpp
    Class& operator=(const Class& a); // 역시 레퍼런스 리턴을 통해 체이닝
                                      // 마찬가지로 return *this를 해주자
    ```


---


# 명품 CPP

## Ch 2

### include: <> vs ""
- <>는 "컴파일러가 설치된 디렉터리"에서 헤더를 찾는 것이다
- ""의 디렉터리는 추가 포함 디렉터리 역시 해당한다


## Ch 3

### 인라인 함수
- 함수 호출 오버헤드를 방지하기 위해 컴파일 단계에서 직접 함수 코드를 삽입하는 함수
- 그러나 code 영역 데이터가 증가하므로 적절하게 사용해야 함
- 강제명령이 아니므로 컴파일러는 필요에 따라 이를 무시 가능


---

