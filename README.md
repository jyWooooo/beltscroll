# 프로젝트 개요
* 장르 : 횡스크롤 방치형 게임
* 개발 기간 : 2024-08-07 ~ 2024-08-11 (4일)
* 개발 도구 : C#, Unity3D, Firebase

# 게임 설명
![image](https://github.com/user-attachments/assets/69a57e88-4005-4c01-aac8-0883416951fd)
![image](https://github.com/user-attachments/assets/2b8b3878-b588-4718-b9da-301e04a4538a)

한 스테이지마다 한 마리의 몬스터가 등장하고, 자동으로 전투하며 몬스터가 죽으면 다음 스테이지로 이동하는 방치형 게임입니다.

# 주요 기능
## SceneManager, SceneBase 클래스를 이용한 씬 로드
첫 SceneBase 클래스가 로드되면, 유니티의 Start() 메서드에서 SceneManager에게 등록됩니다. </br>
이때, 게임은 아직 GameManager를 비롯한 모든 매니저 클래스들이 초기화되지 않은 상태이기 때문에 GameManager 싱글톤 클래스가 호출되고 모든 매니저 클래스들을 초기화하고 데이터를 불러옵니다. </br>
SceneManager는 다른 매니저 클래스가 초기화되는 동안 등록된 SceneBase의 로드를 대기하도록 작성했습니다.

## CSV Data Reader/Converter
CSVReader 클래스는 CSV 파일의 데이터를 읽어 딕셔너리로 변환합니다. </br>
CSVConverter 클래스는 CSVReader를 이용해 CSV 파일을 Scriptable Object 에셋으로 변환합니다.

## 유한 상태 머신
FSM을 이용해 캐릭터와 몬스터의 상태를 관리하고 상태에 맞는 애니메이션을 재생합니다. 

## Firebase Realtime Database
Firebase를 이용해 서버 DB를 이용합니다. </br>
사용자는 게임을 처음 실행하면 User GUID를 생성하고, 이는 로컬 저장소에 저장됩니다. </br>
생성된 User GUID를 이용해 서버 DB에 접근해 몇 스테이지까지 진행했는지 기록하고 불러옵니다.
