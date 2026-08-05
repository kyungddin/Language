
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
