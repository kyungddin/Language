# Python Decorator 이해하기


## Concept
기존 함수를 수정하지 않고 그 기능을 확장하는 방법을 제공


## Ex
```python
# decorator를 정의하는 함수. 인자로 함수를 받습니다.
def my_decorator(func):
    def wrapper():
        print("함수 실행 전 무언가를 합니다..")
        func() # 인자로 받은 함수를 실행합니다.
        print("함수 실행 후 무언가를 합니다.")
    return wrapper

# @ + decorator 함수명을 앞에 쓰는 사용할 수 있습니다.
@my_decorator
def say_hello():
    print("Hello!")
```


## 장단점
- 장점: 코드의 재사용성을 높일 수 있다
    - 특정 로직을 공통적으로 사용하는 여러 함수에 대해 이 로직을 @ 기호와 함께 데코레이터를 추가만 하면 된다?
- 단점: 코드의 복잡도를 높이고, 버그의 원인이 될 수 있다


## 사용 사례
- Flask: @app.route를 통해 특정 URL 요청에 따른 함수를 설정 가능?