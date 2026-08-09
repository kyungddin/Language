
C++ 관련 사항 메모

---
#### 매크로보다는 constexpr

- `#define CONST 10` 이런 식으로 매크로를 사용하면 디버깅 시에 변수명이 안 뜬다
- 따라서 `constexpr int CONST = 10;` 으로 상수를 사용하자

---
#### boost::asio

- boost 라이브러리 중 비동기와 관련됨. 타이머, TCP 통신 등이 가능하다
- `async_*` 계열의 함수를 통해 비동기 큐에 이를 등록하고, `run_one()` 을 써서 pop

---
#### extern 전역 변수와 그 사용

- `extern Object g_obj;` 이런 식으로 전역 변수를 헤더에서 선언했다면
- `extern` 키워드를 사용해서 이를 사용하는 것보다, 해당 헤더를 include 하는 것이 낫다

---
#### lambda 함수

- 정리하기

---
#### 스마트 포인터

- 정리하기 (unique_ptr 부터)

---
#### const 함수

- 반환 타입에 const를 붙이면 외부에서 수정 불가
- 함수에 const를 붙이면 함수 안에서 수정 불가

---
#### 복사생성자 자동 삭제

- 클래스 멤버 중에 복사가 불가능한 type이 있으면 컴파일러가 복사 생성자를 자동으로 삭제한다

---
#### 함수와 const (반환형, 인자, const 함수)

1. 반환형의 const &
	- &를 사용하면 반환 시 복사 비용 0
	- 그러나 이러면 원본을 바꿀 수도 있음
	- 따라서 const 까지 활용
	- 보통 Getter에 해당 테크닉 사용
	- 다만, 지역변수를 반환하는 경우는 해당 테크닉 사용 시 dangling reference 문제가 있으니 자제

```cpp
// ex
const std::string& getName() const 
{ 
	std::string temp = "hello"; 
	return temp; // temp이 소멸되므로 위험!
}
```

2. 인자의 const &
	- (1)과 같은 이유로

3. 함수 자체의 const
	- 멤버 변수의 변화를 막기 위해 사용한다

---
#### tr1과 boost

- tr1은 C++ 라이브러리에 대한 표준(가이드라인)
- boost는 이를 기반으로 작성한 라이브러리 중 하나

---
#### RAII에 대해

- RAII는 **객체의 수명 관리에 대한 설계** 로 생각하자
- 스마트 포인터를 써도 RAII를 지킨 것이고, 생성자와 소멸자에서 new와 delete 페어를 맞춰도 RAII 패턴 설계다!

---
#### 매크로 대신 const 대신 enum

- 위에서 작성했듯이 const(또는 constexpr)를 쓰는 것이 매크로 보다 낫다
- 그러나 이러한 상수는 참조나 주소를 얻어내어 다른 방식으로 활용될 가능성이 남아 있다
- 그래서 enum 둔갑술을 써서 이를 해결할 수 있다

```cpp
// ex 
enum { constNum = 5 }; // 이런식으로 1개만 enum을 쓰면 마치 상수!
```

---