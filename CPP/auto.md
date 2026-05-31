# C++ auto 자료형 정리


## 참조를 놓치는 경우

```cpp
vector<int> v = {1,2,3};
for (auto x : v) x = 0;   // (x) 복사본 수정, 원본 그대로
for (auto& x : v) x = 0;  // (o) 참조로 받아야 원본 수정
```


## auto가 위험한 상황

```cpp
// 프록시 객체 - vector<bool> 의 함정
// vector<bool>은 메모리 절약을 위해 비트 단위로 압축 저장하므로, 실제 bool과 좀 다름..
vector<bool> v = {true, false};
auto val = v[0];   // bool이 아니라 __bit_reference 라는 내부 타입으로 추론됨
bool val = v[0];   // 이게 안전
```

## auto 권장사항
- Google: 타입이 명확하거나 길면 auto 써라
- 이터레이터, size_t, 람다 같은 특이 케이스에는 auto가 효과적
- 임시 변수에도 auto가 효과적


#### 번외 : 범위 기반 for 문

```cpp
vector<int> v = {1, 2, 3};

// 복사 - 원본 안 바뀜
for (int x : v) {
    x = 0;  // v는 그대로
}

// 참조 - 원본 바뀜
for (int& x : v) {
    x = 0;  // v = {0, 0, 0}
}

// const 참조 - 읽기 전용 + 복사 비용 없음
for (const int& x : v) {
    cout << x;
}
```