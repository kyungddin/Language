# Celery 개념 잡고 가기


## 서버: 동기식 작업 vs 비동기식 작업
- 동기식: 요청이 오면 그 요청을 처리하는 동안 서버가 묶임
- 비동기식: 요청은 큐에 넣기, 처리는 다른 쪽에서 함.
    - 즉 서버는 계속해서 요청을 받아낼 수 있음


## celery는 서버? 프로세스?
celery는 명확한 프로세스다. 단 이를 터미널에서 실행하면 아무것도 할 수 없으니 백그라운드 ㄱㄱ


## celery의 broker & backend
- broker: celery가 지정한 작업큐 (우리는 redis를 사용)
- backend: celery의 결과를 저장하는 저장소


## celery의 worker
- celery의 worker의 경우 bash에서 celery를 실행하면 생성되는 프로세스이다
- 이는 broker를 계속해서 감시하며, task가 broker에 들어오면 처리한다


## 서버와 프로세스
- flask와 redis는 서버이다 
- celery는 백그라운드 프로세스이다