C++은 잘 쓰면 매우 빠르지만, **특정 패턴을 잘못 쓰면 확실하게 성능이 떨어진다.**
실제로 실무/대회에서 자주 문제 되는 것들만 핵심 위주로 정리해줄게.

---

# C++에서 진짜 성능 떨어지는 패턴

## 1. 불필요한 복사 (가장 흔하고 치명적)

```cpp
std::vector<int> foo() {
    std::vector<int> v(1000000);
    return v;  // 복사 발생 가능
}
```

또는

```cpp
void func(std::vector<int> v) {  // 값 전달 (복사)
```

### 문제

* 큰 객체 복사 → 메모리 + 시간 낭비
* 특히 vector, string, map 등

### 해결

```cpp
void func(const std::vector<int>& v)  // 참조 전달
```

또는

```cpp
return std::move(v);
```

---

## 2. virtual 함수 남용

```cpp
class Base {
public:
    virtual void foo();
};
```

### 문제

* 호출 시 vtable lookup 발생 (간접 호출)
* 인라인 최적화 불가능

### 영향

* 루프 안에서 호출하면 성능 크게 떨어짐

### 해결

* 꼭 필요할 때만 사용
* 성능 중요하면 CRTP / static polymorphism 고려

---

## 3. cache locality 무시 (컨테이너 선택 실수)

```cpp
std::list<int> lst;
```

### 문제

* list는 포인터 기반 → 메모리 흩어짐
* CPU cache miss 증가 → 매우 느림

### 비교

* `std::vector` 👉 빠름 (연속 메모리)
* `std::list` 👉 느림

### 결론

👉 특별한 이유 없으면 무조건 vector

---

## 4. reserve 안 해서 재할당 폭발

```cpp
std::vector<int> v;
for (int i = 0; i < 1000000; i++)
    v.push_back(i);
```

### 문제

* capacity 부족 → realloc 반복
* 복사 + 메모리 재할당 계속 발생

### 해결

```cpp
v.reserve(1000000);
```

---

## 5. string 과도한 생성 / concat

```cpp
std::string s;
for (...) {
    s += "hello";  // 계속 realloc
}
```

### 문제

* 문자열 재할당 반복
* O(n²) 될 수 있음

### 해결

```cpp
s.reserve(...);
```

또는

```cpp
std::ostringstream
```

---

## 6. map 남용 (unordered_map vs map)

```cpp
std::map<int, int> m;
```

### 문제

* map = red-black tree → O(log n)
* cache locality도 안 좋음

### 대안

```cpp
std::unordered_map<int, int>
```

👉 평균 O(1)

---

## 7. 불필요한 동적 할당 (new/delete)

```cpp
for (...) {
    int* x = new int;
}
```

### 문제

* heap allocation 매우 느림
* fragmentation 발생

### 해결

* stack 사용
* object pool / allocator

---

## 8. 작은 함수인데 inline 안 됨

```cpp
int add(int a, int b) { return a + b; }
```

### 문제

* 함수 호출 오버헤드

### 해결

```cpp
inline int add(int a, int b)
```

(요즘은 컴파일러가 자동으로 잘 해주긴 함)

---

## 9. 불필요한 초기화 / 생성

```cpp
std::vector<int> v(1000000, 0);  // 필요 없는데 초기화
```

### 문제

* 시간 낭비

---

## 10. range-based for에서 복사 발생

```cpp
for (auto x : vec)  // 복사!
```

### 해결

```cpp
for (auto& x : vec)
```

---

# 핵심 요약

진짜 성능 떨어지는 이유는 딱 4가지로 정리됨:

### 1. 메모리

* 복사
* 할당
* cache miss

### 2. 추상화 비용

* virtual
* dynamic dispatch

### 3. 자료구조 선택

* list, map 남용

### 4. 컴파일러 최적화 방해

* inline 안됨
* 불필요한 객체

---

# 🎯 한 줄 정리

**“C++이 느린 게 아니라, 비용을 숨겨놓은 기능을 무심코 쓰면 느려진다”**