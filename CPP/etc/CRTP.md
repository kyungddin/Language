# CRTP(Curiously Recurring Template Pattern)
- C++ 성능 최적화 및 고급 테크닉 중 하나
    - https://cppdeveloper.tistory.com/entry/C-%EC%84%B1%EB%8A%A5-%EC%B5%9C%EC%A0%81%ED%99%94-%EB%B0%8F-%EA%B3%A0%EA%B8%89-%ED%85%8C%ED%81%AC%EB%8B%89-Day-14-CRTP-Curiously-Recurring-Template-Pattern-%EC%82%AC%EC%9A%A9%EB%B2%95

- 기본 클래스가 자신을 상속하는 파생 클래스를 템플릿 매개변수로 받는 패턴
    - 쉽게 말해서.. 다형성을 템플릿과 다운캐스팅으로 하는 것 (기존은 virutal + upcatsing)
    - 코드 재사용성과 컴파일 시간 다형성 구현 가능


## CRTP의 기본 구조
```cpp
template <typename Derived>
class Base {
public:
    void interface() {
        static_cast<Derived*>(this)->implementation();
    }

    void implementation() {
        std::cout << "Base implementation" << std::endl;
    }
};

class Derived : public Base<Derived> { // 바로 이 부분이 핵심이다!
public:
    void implementation() {
        std::cout << "Derived implementation" << std::endl;
    }
};
```


## virtual vs CRTP
- 일반적인 경우에는 당연히 virtual이 훌륭하다
- 다만 특수한 상황이다 성능을 요구하는 부분에서는 CRTP가 유리
- virtual의 치명적 한계 3가지
    - 인라이닝이 절대 안 됨 (컴파일 타임에 타입 확정)
    - vtable 비용 + 분기 예측 실패 (CPU 캐시 미스?)
    - 헤더 기반 템플릿 라이브러리와 궁합 (Eigen, Boost 같은 곳에서는 virtual 못 씀?)
