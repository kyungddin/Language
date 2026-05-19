# 260519
- CSharp 교육 Day1


# C#은 5세대 언어
- .net 프레임워크로 다양한 기능을 지원
- CBD(COmponent Based Development)
    - 컴포넌트 단위로 조립해서 개발하는 방법론

# using() {}
- 종료 시점에 IDisposable의 Dispose()가 호출된다
    - IDisposable은 인터페이스
    - Dispose() 메서드만 존재한다
    - using()의 괄호 안에서 보통 객체를 선언한다
        - 그리고 {} 안에서 객체의 동작 발생


# 제네릭
- C#의 템플릿


# WinForm 클래스를 통해 Partial 공부 가능


# Object 클래스
- C#의 모든 클래스들은 Object 클래스를 상속한다
- Object 메서드
    - ToString()
    - GetType()
    - GetHashCode()
    - Equals()
    - 이러한 메서드들을 override로 재정의 가능하다



# Property 선언
- 클래스 안에서 {타입, 이름, get, set} 만 있으면 컴파일러가 Property로 판단
    - get / set 중 하나만 있는 경우도 존재


# 하이딩(new) vs 오버라이딩(override)
- 하이딩은 참조 타입을 기준으로 동작한다
- 오버라이딩은 동적 바인딩이 일어나서 실제 타입을 기준으로 동작한다


# Attribute
- 내장 Attribute
    - Obsolete: 사용 중단 경고
    - Serializable: 직렬화 가능 표시
    - DllImport: 외부 DLL 함수 가져오기

- 직접 만들기
    ```cs
    // 1. Attribute 클래스 상속
    class MyAttribute : Attribute
    {
        public string Description { get; set; }
        public int Version { get; set; }
    }

    // 2. 붙여서 사용
    [MyAttribute(Description = "테스트 클래스", Version = 1)]
    class MyClass { }
    ```
    - 리플렉션으로 읽어야 활용 가능?
    - 당장은 너무나 어려운 개념


# Dispose() 구현 예제

    ```cs
    class DatabaseManager : IDisposable
    {
        private SqlConnection cn;
        private bool disposed = false;  // 중복 호출 방지

        public DatabaseManager(string connectionString)
        {
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        public void Query(string sql)
        {
            // 쿼리 작업
        }

        public void Dispose()
        {
            if (!disposed)
            {
                cn.Close();
                cn.Dispose();
                disposed = true;
            }
        }
    }

    // 사용
    using (var db = new DatabaseManager("connectionString"))
    {
        db.Query("SELECT * FROM ...");
    }  
    ```
    - Dispose()는 단순히 GC의 메모리 처리 기능뿐만 아니라 사용하지 않는 컴포넌트에 대한 구체적인 처리가 가능하기에 유용!


# Class vs Struct
- Class는 레퍼런스 타입
- Struct는 값 타입


# 인터페이스
- 다중 상속을 허용 (원래 C#은 다중상속이 안댐)
- 프로퍼티도 인터페이스에 추가 가능하지만 
    - { get; set; } 이런식으로밖에 구현을 못함


# this 오버라이딩을 통한 인덱스 함수 커스터마이징
    ```cs
    class SquareCalculator
    {
        public int this[int t]
        {
            get
            {
                return i * i;
            }
        }
    }
    ```
    - 이렇게 하면 Array가 아니어도 인덱스를 이용한 동작을 구현 가능!


# C#의 인터페이스를 보며 다형성을 기르려는 노력을 하자!


# 인터페이스 특
- 조금 많이 특별한.. 부모 클래스라고 생각하자
    - 다중 상속을 지원한다
    - 구현이 불가능하다
