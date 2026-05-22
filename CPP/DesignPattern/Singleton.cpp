/*
Singleton Pattern
- 장점
    - 객체의 유일성 보장
    - 데이터 공유
    - 메모리 절약
- 단점
    - 알아보기  
*/


// Example 1

#include <iostream>

class Singleton {
private:    // 생성자, 복사 생성자, 대입 연산자, 소멸자를 private으로 선언하여 외부에서 객체 생성을 막음
    Singleton() {}
    Singleton(const Singleton& ref) {}
    Singleton& operator=(const Singleton& ref) {}
    ~Singleton() {}
public:     // getInstance() 함수를 통해 객체에 접근할 수 있도록 함
    static Singleton& getInstance() {
        static Singleton s;
        return s;
    }
};

int main(void) {
    Singleton& s = Singleton::getInstance();   // static 참조를 쓰는 것이 포인터보다 좋은 구현
    return 0;
}


// Example 2

// 싱글톤 클래스
class Singleton {

public:
	// static 함수로 선언
	static Singleton& GetInstance() {
		// staitc 변수로 선언함으로서, instance 변수는 한번만 초기화되고, 프로그램 수명 내내 지속됨.
		// 특히 C++11부터 thread-safe 변수 초기화가 보장됨.
 		static Singleton instance;
		return instance;
	}

private:
	// Default 생성자 사용 (필요시 생성자를 원하는데로 수정해서 사용해도 됨)
	Singleton() = default;

	// 객체는 유일하게 하나만 생성되어야 하기에 복사(대입), 이동(대입) 생성자 비활성화
	// 복사, 이동 생성자를 delete로 선언함으로서, 
	// 프로그래머 실수에 의한 복사, 이동 생성자 호출을 원천에 방지할 수 있음.
	Singleton(const Singleton&) = delete;
	Singleton& operator=(const Singleton&) = delete;
	Singleton(Singleton&&) = delete;
	Singleton& operator=(Singleton&&) = delete;
};


// 사용 방법
int main(void) {
	auto& singleton = Singleton::GetInstance();
}


// SubClass Example
#include <iostream>
using namespace std;

// 인터페이스 역할만 하는 베이스 클래스
class Database {
public:
    virtual void Connect() = 0;    // 순수 가상 함수 → 직접 인스턴스 생성 불가
    virtual void Query() = 0;
    virtual void Disconnect() = 0;

    virtual ~Database() = default;

protected:
    Database() = default;          // 서브클래스만 생성 가능

private:
    Database(const Database&) = delete;
    Database& operator=(const Database&) = delete;
};

// 실제 구현 싱글톤
class MySQL : public Database {
public:
    static MySQL& GetInstance() {
        static MySQL instance;
        return instance;
    }

    void Connect() override    { cout << "MySQL Connect" << endl; }
    void Query() override      { cout << "MySQL Query" << endl; }
    void Disconnect() override { cout << "MySQL Disconnect" << endl; }

private:
    MySQL() = default;
    MySQL(const MySQL&) = delete;
    MySQL& operator=(const MySQL&) = delete;
};

class PostgreSQL : public Database {
public:
    static PostgreSQL& GetInstance() {
        static PostgreSQL instance;
        return instance;
    }

    void Connect() override    { cout << "PostgreSQL Connect" << endl; }
    void Query() override      { cout << "PostgreSQL Query" << endl; }
    void Disconnect() override { cout << "PostgreSQL Disconnect" << endl; }

private:
    PostgreSQL() = default;
    PostgreSQL(const PostgreSQL&) = delete;
    PostgreSQL& operator=(const PostgreSQL&) = delete;
};

int main() {
    // Database db; // ❌ 순수 가상 함수 때문에 직접 생성 불가

    Database& db = MySQL::GetInstance(); // ✅ 베이스 참조로 받을 수 있음
    db.Connect();
    db.Query();
}


// 싱글톤에서 업캐스팅 자체는 어색하다.. GetInstance() 자체가 ::를 통해 타입을 특정하고 있기 때문이다
