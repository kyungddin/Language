using System.Diagnostics;

namespace MultithreadingDemo;

// ============================================================
//  C# WinForm 멀티스레딩 교육 데모
//  개념: Thread / Task / 동기·비동기 / Blocking·Non-Blocking / 스레드 동기화
// ============================================================
public class MainForm : Form
{
    // ── 탭 로그 박스
    private RichTextBox rtbThread = null!;
    private RichTextBox rtbTask   = null!;
    private RichTextBox rtbSync   = null!;
    private RichTextBox rtbBlock  = null!;
    private RichTextBox rtbLock   = null!;

    // ── 동기 vs 비동기 탭 전용
    private ProgressBar pbSync    = null!;
    private Button      btnSyncGo = null!;
    private Button      btnAsnGo  = null!;

    // ── 스레드 동기화 탭 전용
    private Label lblCounter = null!;

    // ── 취소 토큰 (Task 탭)
    private CancellationTokenSource? cts;

    // ── lock 오브젝트 (스레드 동기화 탭)
    private readonly object _lock = new();

    // ── SplitContainer 참조 (OnShown에서 SplitterDistance 설정)
    private SplitContainer scThread = null!;
    private SplitContainer scTask   = null!;
    private SplitContainer scSync   = null!;
    private SplitContainer scBlock  = null!;
    private SplitContainer scLock   = null!;

    public MainForm()
    {
        Text          = "C# 멀티스레딩 교육 데모";
        Size          = new Size(1040, 760);
        MinimumSize   = new Size(860, 620);
        StartPosition = FormStartPosition.CenterScreen;
        Font          = new Font("Malgun Gothic", 9.5f);

        var tab = new TabControl { Dock = DockStyle.Fill, Font = new Font("Malgun Gothic", 9.5f) };
        tab.TabPages.Add(BuildThreadTab());
        tab.TabPages.Add(BuildTaskTab());
        tab.TabPages.Add(BuildSyncAsyncTab());
        tab.TabPages.Add(BuildBlockingTab());
        tab.TabPages.Add(BuildLockTab());
        Controls.Add(tab);
    }

    // SplitterDistance는 Width=0인 생성자에서 설정 불가 → OnShown에서 처리
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        foreach (var sc in new[] { scThread, scTask, scSync })
        {
            sc.Panel1MinSize    = 290;
            sc.Panel2MinSize    = 340;
            sc.SplitterDistance = 300;
        }
        foreach (var sc in new[] { scBlock, scLock })
        {
            sc.Panel1MinSize    = 300;
            sc.Panel2MinSize    = 340;
            sc.SplitterDistance = 315;
        }
    }

    // 미방문 탭의 핸들을 미리 생성해두어 백그라운드 스레드 Invoke가 동작하도록 보장
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        _ = rtbThread.Handle;
        _ = rtbTask.Handle;
        _ = rtbSync.Handle;
        _ = rtbBlock.Handle;
        _ = rtbLock.Handle;
        _ = lblCounter.Handle;
    }

    // ════════════════════════════════════════════════════════
    //  공통 헬퍼
    // ════════════════════════════════════════════════════════

    // InvokeRequired는 핸들이 없으면 잘못된 false 반환 → IsHandleCreated도 이중 확인
    private void Log(RichTextBox rtb, string msg, Color? col = null)
    {
        var tid  = Thread.CurrentThread.ManagedThreadId;
        var time = DateTime.Now.ToString("HH:mm:ss.fff");
        var line = $"[{time}][T-{tid:D2}] {msg}\n";
        var c    = col ?? Color.LightGray;

        Action append = () =>
        {
            if (rtb.IsDisposed) return;
            rtb.SelectionStart  = rtb.TextLength;
            rtb.SelectionLength = 0;
            rtb.SelectionColor  = c;
            rtb.AppendText(line);
            rtb.ScrollToCaret();
        };

        if (rtb.IsDisposed) return;
        if (!rtb.IsHandleCreated) { rtb.BeginInvoke(append); return; }
        if (rtb.InvokeRequired)   rtb.Invoke(append);
        else                       append();
    }

    // 외부 Panel: Dock=Fill + AutoScroll → 스크롤 영역 담당
    // 내부 FlowLayoutPanel: AutoSize=true → 컨트롤 추가에 따라 높이 자동 확장
    // (FlowLayoutPanel 자체를 Dock=Fill로 쓰면 AutoScroll 계산이 깨지는 WinForms 버그 우회)
    private static (Panel outer, FlowLayoutPanel inner) MakeLeft()
    {
        var flow = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.TopDown,
            WrapContents  = false,
            AutoSize      = true,
            AutoSizeMode  = AutoSizeMode.GrowAndShrink,
            Padding       = new Padding(10, 12, 6, 12),
        };
        var panel = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
        panel.Controls.Add(flow);
        return (panel, flow);
    }

    private static SplitContainer MakeSplit() => new() { Dock = DockStyle.Fill };

    private static RichTextBox MakeLog() => new()
    {
        Dock        = DockStyle.Fill,
        ReadOnly    = true,
        BackColor   = Color.FromArgb(20, 20, 30),
        ForeColor   = Color.LightGray,
        Font        = new Font("Consolas", 9f),
        BorderStyle = BorderStyle.None,
        ScrollBars  = RichTextBoxScrollBars.Vertical,
    };

    // 제목 라벨
    private static Label MakeTitle(string text) => new()
    {
        Text      = text,
        Font      = new Font("Malgun Gothic", 11f, FontStyle.Bold),
        ForeColor = Color.White,
        AutoSize  = false,
        Size      = new Size(278, 28),
        Margin    = new Padding(0, 0, 0, 8),
    };

    // 설명 라벨 — MaximumSize로 너비 고정, 높이는 자동
    private static Label MakeDesc(string text) => new()
    {
        Text        = text,
        AutoSize    = true,
        MaximumSize = new Size(278, 0),
        ForeColor   = Color.FromArgb(170, 170, 170),
        Margin      = new Padding(0, 0, 0, 12),
    };

    // 구분선
    private static Label MakeSep() => new()
    {
        AutoSize    = false,
        Size        = new Size(278, 1),
        BackColor   = Color.FromArgb(60, 60, 70),
        Margin      = new Padding(0, 6, 0, 10),
    };

    // 버튼 — FlowLayout이 위치 결정, Margin으로 간격 조정
    private static Button MakeBtn(string text, Color? color = null) => new()
    {
        Text      = text,
        Size      = new Size(278, 36),
        FlatStyle = FlatStyle.Flat,
        BackColor = color ?? Color.FromArgb(50, 100, 160),
        ForeColor = Color.White,
        TextAlign = ContentAlignment.MiddleLeft,
        Padding   = new Padding(8, 0, 0, 0),
        Margin    = new Padding(0, 0, 0, 8),
    };

    // ════════════════════════════════════════════════════════
    //  탭 1 : Thread 기초
    // ════════════════════════════════════════════════════════
    private TabPage BuildThreadTab()
    {
        var page = new TabPage("① Thread 기초");
        var sc   = scThread = MakeSplit();
        var (leftPanel, left) = MakeLeft();
        rtbThread = MakeLog();

        var b1 = MakeBtn("1. Thread 생성 & 시작");
        var b2 = MakeBtn("2. Thread.Sleep (UI 스레드 블로킹)");
        var b3 = MakeBtn("3. Thread.Join 대기");
        var b4 = MakeBtn("4. 람다 캡처로 매개변수 전달");
        var b5 = MakeBtn("5. Foreground vs Background");
        var b6 = MakeBtn("6. 여러 Thread 동시 실행");
        var bc = MakeBtn("[ 로그 초기화 ]", Color.FromArgb(70, 70, 80));

        b1.Click += (_, _) =>
        {
            Log(rtbThread, "══ Thread 생성 & 시작 ══", Color.Cyan);
            Log(rtbThread, "메인(UI) 스레드에서 새 Thread 생성 중...", Color.SkyBlue);

            var t = new Thread(() =>
            {
                Log(rtbThread, "▶ 새 Thread 실행 시작!", Color.Gold);
                for (int i = 1; i <= 5; i++)
                {
                    Thread.Sleep(500);
                    Log(rtbThread, $"  작업 {i}/5 진행 중...", Color.Orange);
                }
                Log(rtbThread, "▶ Thread 완료", Color.Gold);
            });
            t.Name = "Worker-1";

            Log(rtbThread, $"Start 호출 전 상태: {t.ThreadState}", Color.Plum);
            t.Start();
            Log(rtbThread, $"Start 호출 후 상태: {t.ThreadState}", Color.Plum);
            Log(rtbThread, "메인 스레드 계속 실행 중 → UI 응답 가능", Color.SkyBlue);
        };

        b2.Click += (_, _) =>
        {
            Log(rtbThread, "══ Thread.Sleep (UI 블로킹) ══", Color.Cyan);
            Log(rtbThread, "⚠ UI 스레드를 3초 동안 직접 Sleep합니다!", Color.OrangeRed);
            Log(rtbThread, "지금 창을 클릭하거나 드래그해 보세요 → 반응 없음", Color.OrangeRed);
            Thread.Sleep(3000);   // UI 스레드 직접 차단
            Log(rtbThread, "✔ 3초 후 Sleep 종료 → UI 다시 응답", Color.LimeGreen);
        };

        b3.Click += (_, _) =>
        {
            Log(rtbThread, "══ Thread.Join 대기 ══", Color.Cyan);
            Log(rtbThread, "Join은 호출 스레드를 차단합니다. Task.Run 안에서 시연합니다.", Color.SkyBlue);
            Task.Run(() =>
            {
                var worker = new Thread(() =>
                {
                    Log(rtbThread, "Worker: 3초 작업 시작", Color.Gold);
                    Thread.Sleep(3000);
                    Log(rtbThread, "Worker: 작업 완료", Color.Gold);
                });
                worker.Start();
                Log(rtbThread, "🔴 [Blocking] Join 호출 → 이 스레드 차단됨", Color.OrangeRed);
                worker.Join();
                Log(rtbThread, "✔ Join 반환 → 차단 해제", Color.LimeGreen);
            });
        };

        b4.Click += (_, _) =>
        {
            Log(rtbThread, "══ 람다 캡처로 매개변수 전달 ══", Color.Cyan);
            string msg = "람다로 캡처한 문자열";
            int number = 42;
            new Thread(() => Log(rtbThread, $"T1 수신 → msg={msg}, number={number}", Color.Gold)).Start();

            // 반복문 캡처 버그 시연: i를 직접 쓰면 루프 종료 후 값(3)이 모두 동일하게 캡처됨
            for (int i = 0; i < 3; i++)
            {
                int captured = i;   // ← 반드시 로컬 복사
                new Thread(() => Log(rtbThread, $"Loop Thread captured = {captured}", Color.Orange)).Start();
            }
        };

        b5.Click += (_, _) =>
        {
            Log(rtbThread, "══ Foreground vs Background Thread ══", Color.Cyan);
            new Thread(() =>
            {
                Log(rtbThread, $"[Foreground] IsBackground={Thread.CurrentThread.IsBackground} → 앱 종료 막음", Color.Gold);
                Thread.Sleep(2000);
                Log(rtbThread, "[Foreground] 완료", Color.Gold);
            }) { IsBackground = false }.Start();

            new Thread(() =>
            {
                Log(rtbThread, $"[Background] IsBackground={Thread.CurrentThread.IsBackground} → 앱 종료 시 강제 종료", Color.SkyBlue);
                Thread.Sleep(2000);
                Log(rtbThread, "[Background] 완료", Color.SkyBlue);
            }) { IsBackground = true }.Start();
        };

        b6.Click += (_, _) =>
        {
            Log(rtbThread, "══ 여러 Thread 동시 실행 ══", Color.Cyan);
            Log(rtbThread, "5개 Thread 시작 — 순서는 OS 스케줄러가 결정", Color.SkyBlue);
            var colors = new[] { Color.Red, Color.DeepSkyBlue, Color.LimeGreen, Color.Violet, Color.Gold };
            var rng    = new Random();
            for (int i = 0; i < 5; i++)
            {
                int idx = i; Color col = colors[i];
                new Thread(() =>
                {
                    int ms = rng.Next(300, 1800);
                    Log(rtbThread, $"Thread-{idx + 1} 시작 (약 {ms}ms)", col);
                    Thread.Sleep(ms);
                    Log(rtbThread, $"Thread-{idx + 1} 완료", col);
                }).Start();
            }
        };

        bc.Click += (_, _) => rtbThread.Clear();

        left.Controls.Add(MakeTitle("Thread 클래스 기초"));
        left.Controls.Add(MakeDesc(
            "Thread는 OS 수준의 스레드를 직접 제어합니다.\n" +
            "• Foreground : 앱 종료를 막음\n" +
            "• Background : 앱 종료 시 함께 종료\n" +
            "• UI 스레드를 직접 Sleep하면 UI 멈춤!"));
        left.Controls.Add(MakeSep());
        left.Controls.AddRange(new Control[] { b1, b2, b3, b4, b5, b6, MakeSep(), bc });

        sc.Panel1.Controls.Add(leftPanel);
        sc.Panel2.Controls.Add(rtbThread);
        page.Controls.Add(sc);
        return page;
    }

    // ════════════════════════════════════════════════════════
    //  탭 2 : Task 기초
    // ════════════════════════════════════════════════════════
    private TabPage BuildTaskTab()
    {
        var page = new TabPage("② Task 기초");
        var sc   = scTask = MakeSplit();
        var (leftPanel, left) = MakeLeft();
        rtbTask  = MakeLog();

        var b1 = MakeBtn("1. Task.Run 기본");
        var b2 = MakeBtn("2. Task<T> 결과값 반환");
        var b3 = MakeBtn("3. Task.WhenAll 병렬 실행");
        var b4 = MakeBtn("4. Task.WhenAny 첫 완료 감지");
        var b5 = MakeBtn("5. ContinueWith 파이프라인");
        var b6 = MakeBtn("6. CancellationToken 취소");
        var bx = MakeBtn("   ⛔ 취소 신호 전송", Color.FromArgb(160, 40, 40));
        var bc = MakeBtn("[ 로그 초기화 ]", Color.FromArgb(70, 70, 80));

        b1.Click += (_, _) =>
        {
            Log(rtbTask, "══ Task.Run 기본 ══", Color.Cyan);
            Log(rtbTask, "Task.Run → ThreadPool에서 스레드 할당 후 실행", Color.SkyBlue);
            Task.Run(() =>
            {
                Log(rtbTask, "▶ Task 실행 중 (ThreadPool 스레드)", Color.Gold);
                Thread.Sleep(1500);
                Log(rtbTask, "▶ Task 완료", Color.Gold);
            });
            Log(rtbTask, "Task.Run 호출 직후 즉시 반환 → UI 응답 유지", Color.SkyBlue);
        };

        b2.Click += async (_, _) =>
        {
            Log(rtbTask, "══ Task<T> 결과값 반환 ══", Color.Cyan);
            int sum = await Task.Run(() =>
            {
                Log(rtbTask, "계산 중 (2초)...", Color.Gold);
                Thread.Sleep(2000);
                return 100 + 200;
            });
            Log(rtbTask, $"✔ int 결과: {sum}", Color.LimeGreen);

            string text = await Task.Run(() => { Thread.Sleep(300); return "Task가 반환한 문자열"; });
            Log(rtbTask, $"✔ string 결과: {text}", Color.LimeGreen);
        };

        b3.Click += async (_, _) =>
        {
            Log(rtbTask, "══ Task.WhenAll — 병렬 실행 후 전체 완료 대기 ══", Color.Cyan);
            var sw = Stopwatch.StartNew();
            var t1 = Task.Run(() => { Thread.Sleep(1000); Log(rtbTask, "Task1 완료 (1초)", Color.Red);    return 1; });
            var t2 = Task.Run(() => { Thread.Sleep(2000); Log(rtbTask, "Task2 완료 (2초)", Color.Cyan);   return 2; });
            var t3 = Task.Run(() => { Thread.Sleep(1500); Log(rtbTask, "Task3 완료 (1.5초)", Color.Gold); return 3; });
            int[] results = await Task.WhenAll(t1, t2, t3);
            sw.Stop();
            Log(rtbTask, $"✔ 전체 완료! 결과: [{string.Join(", ", results)}]", Color.LimeGreen);
            Log(rtbTask, $"⏱ 경과: {sw.ElapsedMilliseconds}ms (순차 시 4500ms)", Color.Plum);
        };

        b4.Click += async (_, _) =>
        {
            Log(rtbTask, "══ Task.WhenAny — 가장 먼저 완료된 Task ══", Color.Cyan);
            var t1 = Task.Run(() => { Thread.Sleep(3000); return "Task1(3초)"; });
            var t2 = Task.Run(() => { Thread.Sleep(1000); return "Task2(1초)"; });
            var t3 = Task.Run(() => { Thread.Sleep(2000); return "Task3(2초)"; });
            Log(rtbTask, "3개 Task 시작, 첫 번째 완료를 기다립니다...", Color.SkyBlue);
            var winner = await Task.WhenAny(t1, t2, t3);
            Log(rtbTask, $"🏆 최초 완료: {await winner}", Color.LimeGreen);
            Log(rtbTask, "나머지 Task는 백그라운드에서 계속 실행됩니다", Color.Gray);
        };

        b5.Click += (_, _) =>
        {
            Log(rtbTask, "══ ContinueWith 파이프라인 ══", Color.Cyan);
            Log(rtbTask, "Task 완료 후 자동으로 다음 단계 실행 (Non-Blocking)", Color.SkyBlue);
            Task.Run(() => { Log(rtbTask, "1단계: 데이터 로드 (1초)", Color.Gold); Thread.Sleep(1000); return "로드된 데이터"; })
                .ContinueWith(prev => { Log(rtbTask, $"2단계: 처리 중 ← {prev.Result}", Color.SkyBlue); Thread.Sleep(800); return prev.Result + " → 처리완료"; })
                .ContinueWith(prev => { Log(rtbTask, $"3단계: 저장 ← {prev.Result}", Color.Plum); Thread.Sleep(400); })
                .ContinueWith(_ => Log(rtbTask, "✔ 파이프라인 전체 완료!", Color.LimeGreen));
        };

        b6.Click += async (_, _) =>
        {
            Log(rtbTask, "══ CancellationToken 취소 ══", Color.Cyan);
            Log(rtbTask, "실행 중 '⛔ 취소 신호' 버튼을 눌러보세요", Color.SkyBlue);
            cts = new CancellationTokenSource();
            var token = cts.Token;
            try
            {
                await Task.Run(() =>
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        token.ThrowIfCancellationRequested();
                        Log(rtbTask, $"진행 {i}/10...", Color.Gold);
                        Thread.Sleep(600);
                    }
                }, token);
                Log(rtbTask, "✔ 정상 완료", Color.LimeGreen);
            }
            catch (OperationCanceledException)
            {
                Log(rtbTask, "⛔ 작업이 취소되었습니다", Color.OrangeRed);
            }
            finally { cts.Dispose(); cts = null; }
        };

        bx.Click += (_, _) => { cts?.Cancel(); Log(rtbTask, "⛔ Cancel() 호출!", Color.OrangeRed); };
        bc.Click += (_, _) => rtbTask.Clear();

        left.Controls.Add(MakeTitle("Task 클래스 기초"));
        left.Controls.Add(MakeDesc(
            "Task는 Thread보다 고수준 추상화입니다.\n" +
            "• ThreadPool 재사용 → 효율적\n" +
            "• async/await와 완벽 통합\n" +
            "• 취소(CancellationToken) 내장 지원"));
        left.Controls.Add(MakeSep());
        left.Controls.AddRange(new Control[] { b1, b2, b3, b4, b5, b6, bx, MakeSep(), bc });

        sc.Panel1.Controls.Add(leftPanel);
        sc.Panel2.Controls.Add(rtbTask);
        page.Controls.Add(sc);
        return page;
    }

    // ════════════════════════════════════════════════════════
    //  탭 3 : 동기 vs 비동기
    // ════════════════════════════════════════════════════════
    private TabPage BuildSyncAsyncTab()
    {
        var page = new TabPage("③ 동기 vs 비동기");
        var sc   = scSync = MakeSplit();
        var (leftPanel, left) = MakeLeft();
        rtbSync  = MakeLog();

        pbSync = new ProgressBar
        {
            Size   = new Size(276, 18),
            Margin = new Padding(0, 0, 0, 6),
        };

        var btnTest = new Button
        {
            Text      = "▶ UI 반응 테스트 버튼 (실행 중 클릭해보세요)",
            Size      = new Size(276, 28),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(30, 110, 50),
            ForeColor = Color.White,
            TextAlign = ContentAlignment.MiddleLeft,
            Padding   = new Padding(6, 0, 0, 0),
            Margin    = new Padding(0, 0, 0, 6),
        };
        btnTest.Click += (_, _) => Log(rtbSync, "✔ UI가 지금 응답 중입니다!", Color.LimeGreen);

        btnSyncGo = MakeBtn("1. 동기 실행 (UI 블로킹 체험)");
        btnAsnGo  = MakeBtn("2. 비동기 실행 (UI 응답 유지)");
        var b3 = MakeBtn("3. async/await + IProgress<T>");
        var b4 = MakeBtn("4. 순차 await vs 병렬 WhenAll");
        var b5 = MakeBtn("5. CPU 집약 vs I/O 대기 차이");
        var bc = MakeBtn("[ 로그 초기화 ]", Color.FromArgb(70, 70, 80));

        // 1. 동기 — UI 스레드 직접 차단
        btnSyncGo.Click += (_, _) =>
        {
            Log(rtbSync, "══ 동기 실행 (UI 블로킹) ══", Color.Cyan);
            Log(rtbSync, "⚠ UI 스레드를 직접 차단합니다. 창이 멈춥니다!", Color.OrangeRed);
            Log(rtbSync, "지금 'UI 반응 테스트' 버튼을 클릭해 보세요 → 무반응", Color.OrangeRed);
            btnSyncGo.Enabled = false;
            for (int i = 0; i <= 100; i += 10)
            {
                pbSync.Value = i;
                Thread.Sleep(300);   // UI 스레드 직접 블로킹
            }
            Log(rtbSync, "✔ 동기 완료 — UI 다시 응답 가능", Color.LimeGreen);
            pbSync.Value      = 0;
            btnSyncGo.Enabled = true;
        };

        // 2. 비동기 — await Task.Delay로 UI 스레드 반환
        btnAsnGo.Click += async (_, _) =>
        {
            Log(rtbSync, "══ 비동기 실행 (UI 응답 유지) ══", Color.Cyan);
            Log(rtbSync, "✔ 실행 중에도 UI가 살아있습니다. 테스트 버튼을 눌러보세요!", Color.LimeGreen);
            btnAsnGo.Enabled = false;
            for (int i = 0; i <= 100; i += 10)
            {
                pbSync.Value = i;
                await Task.Delay(300);   // UI 스레드 반환 후 재개
            }
            Log(rtbSync, "✔ 비동기 완료", Color.LimeGreen);
            pbSync.Value     = 0;
            btnAsnGo.Enabled = true;
        };

        // 3. IProgress<T>로 백그라운드 → UI 진행률 보고
        b3.Click += async (_, _) =>
        {
            Log(rtbSync, "══ async/await + IProgress<T> ══", Color.Cyan);
            Log(rtbSync, "백그라운드 Task → UI 스레드로 진행률 안전하게 보고", Color.SkyBlue);
            var progress = new Progress<(int pct, string step)>(report =>
            {
                pbSync.Value = report.pct;
                Log(rtbSync, $"진행률: {report.pct,3}% — {report.step}", Color.Gold);
            });
            await Task.Run(() =>
            {
                var p = (IProgress<(int, string)>)progress;
                string[] steps = { "데이터 로드", "유효성 검사", "변환", "저장", "완료" };
                for (int i = 0; i < steps.Length; i++)
                {
                    Thread.Sleep(600);
                    p.Report(((i + 1) * 20, steps[i]));
                }
            });
            Log(rtbSync, "✔ 모든 단계 완료", Color.LimeGreen);
            pbSync.Value = 0;
        };

        // 4. 순차 vs 병렬 시간 비교
        b4.Click += async (_, _) =>
        {
            Log(rtbSync, "══ 순차 await vs 병렬 WhenAll ══", Color.Cyan);
            async Task Work(string name, int ms)
            {
                Log(rtbSync, $"  {name} 시작", Color.Gold);
                await Task.Delay(ms);
                Log(rtbSync, $"  {name} 완료", Color.Gold);
            }
            var sw = Stopwatch.StartNew();
            Log(rtbSync, "〔순차 실행 — 총 3초 예상〕", Color.SkyBlue);
            await Work("A", 1000); await Work("B", 1000); await Work("C", 1000);
            Log(rtbSync, $"순차 완료: {sw.ElapsedMilliseconds}ms", Color.OrangeRed);

            sw.Restart();
            Log(rtbSync, "〔병렬 실행 — 총 1초 예상〕", Color.SkyBlue);
            await Task.WhenAll(Work("X", 1000), Work("Y", 1000), Work("Z", 1000));
            Log(rtbSync, $"병렬 완료: {sw.ElapsedMilliseconds}ms", Color.LimeGreen);
        };

        // 5. CPU 집약(Task.Run) vs I/O 대기(await)
        b5.Click += async (_, _) =>
        {
            Log(rtbSync, "══ CPU 집약 vs I/O 대기 ══", Color.Cyan);
            Log(rtbSync, "CPU 집약 → Task.Run (새 스레드 필요)", Color.SkyBlue);
            Log(rtbSync, "I/O 대기  → await    (스레드 점유 없음)", Color.SkyBlue);
            int res = await Task.Run(() => { long s = 0; for (long i = 0; i < 100_000_000L; i++) s += i; return (int)(s % 10000); });
            Log(rtbSync, $"CPU 계산 결과: {res}  (백그라운드 스레드 사용)", Color.Gold);
            await Task.Delay(500);   // 실제론 File.ReadAsync, HttpClient 등
            Log(rtbSync, "I/O 대기 완료 (스레드 낭비 없음)", Color.LimeGreen);
            Log(rtbSync, "→ 고부하 서버에서 async I/O는 스레드 수 대폭 절약", Color.Plum);
        };

        bc.Click += (_, _) => { rtbSync.Clear(); pbSync.Value = 0; };

        left.Controls.Add(MakeTitle("동기(Sync) vs 비동기(Async)"));
        left.Controls.Add(MakeDesc(
            "동기  : 작업 완료까지 현재 스레드 차단\n" +
            "비동기: 완료를 기다리는 동안 스레드 반환\n" +
            "       → UI 응답 유지, 자원 효율 향상"));
        left.Controls.Add(MakeSep());
        left.Controls.Add(pbSync);
        left.Controls.Add(btnTest);
        left.Controls.AddRange(new Control[] { btnSyncGo, btnAsnGo, b3, b4, b5, MakeSep(), bc });

        sc.Panel1.Controls.Add(leftPanel);
        sc.Panel2.Controls.Add(rtbSync);
        page.Controls.Add(sc);
        return page;
    }

    // ════════════════════════════════════════════════════════
    //  탭 4 : Blocking vs Non-Blocking
    // ════════════════════════════════════════════════════════
    private TabPage BuildBlockingTab()
    {
        var page = new TabPage("④ Blocking / Non-Blocking");
        var sc   = scBlock = MakeSplit();
        var (leftPanel, left) = MakeLeft();
        rtbBlock = MakeLog();

        var b1 = MakeBtn("1. Thread.Join       [🔴 Blocking]");
        var b2 = MakeBtn("2. Task.Wait()        [🔴 Blocking]");
        var b3 = MakeBtn("3. task.Result        [🔴 Blocking]");
        var b4 = MakeBtn("4. await              [🟢 Non-Blocking]");
        var b5 = MakeBtn("5. ContinueWith       [🟢 Non-Blocking]");
        var b6 = MakeBtn("6. Deadlock 패턴 설명 (안전)");
        var b7 = MakeBtn("7. 성능 비교: Blocking vs Non-Blocking");
        var bc = MakeBtn("[ 로그 초기화 ]", Color.FromArgb(70, 70, 80));

        b1.Click += (_, _) =>
        {
            Log(rtbBlock, "══ Thread.Join [Blocking] ══", Color.Cyan);
            Task.Run(() =>
            {
                var worker = new Thread(() =>
                {
                    Log(rtbBlock, "Worker: 2초 작업 중...", Color.Gold);
                    Thread.Sleep(2000);
                    Log(rtbBlock, "Worker: 완료", Color.Gold);
                });
                worker.Start();
                Log(rtbBlock, "🔴 Join 호출 → 현재 스레드 차단됨", Color.OrangeRed);
                worker.Join();
                Log(rtbBlock, "✔ Join 반환 → 차단 해제", Color.LimeGreen);
            });
        };

        b2.Click += (_, _) =>
        {
            Log(rtbBlock, "══ Task.Wait() [Blocking] ══", Color.Cyan);
            Task.Run(() =>
            {
                var inner = Task.Run(() =>
                {
                    Log(rtbBlock, "내부 Task: 2초 작업 중...", Color.Gold);
                    Thread.Sleep(2000);
                    Log(rtbBlock, "내부 Task: 완료", Color.Gold);
                });
                Log(rtbBlock, "🔴 Wait() 호출 → 현재 스레드 차단됨", Color.OrangeRed);
                inner.Wait();
                Log(rtbBlock, "✔ Wait() 반환 → 차단 해제", Color.LimeGreen);
            });
        };

        b3.Click += (_, _) =>
        {
            Log(rtbBlock, "══ task.Result [Blocking] ══", Color.Cyan);
            Log(rtbBlock, "⚠ .Result는 결과 준비 전까지 스레드를 차단!", Color.OrangeRed);
            Task.Run(() =>
            {
                var calcTask = Task.Run(() => { Log(rtbBlock, "계산 Task: 2초 작업 중...", Color.Gold); Thread.Sleep(2000); return 42; });
                Log(rtbBlock, "🔴 .Result 접근 → 현재 스레드 차단됨", Color.OrangeRed);
                int val = calcTask.Result;
                Log(rtbBlock, $"✔ 결과: {val} — 차단 해제 (권장: await 사용)", Color.LimeGreen);
            });
        };

        b4.Click += async (_, _) =>
        {
            Log(rtbBlock, "══ await [Non-Blocking] ══", Color.Cyan);
            Log(rtbBlock, "🟢 await는 스레드를 반환한 후 완료 시 재개합니다", Color.LimeGreen);
            Log(rtbBlock, $"await 전 — UI 스레드 ID: {Thread.CurrentThread.ManagedThreadId}", Color.SkyBlue);
            int val = await Task.Run(() => { Log(rtbBlock, "작업 스레드에서 실행 중...", Color.Gold); Thread.Sleep(2000); return 99; });
            Log(rtbBlock, $"await 후 — UI 스레드 ID: {Thread.CurrentThread.ManagedThreadId}", Color.SkyBlue);
            Log(rtbBlock, $"✔ 결과: {val}  (대기 중 UI 응답 가능했음)", Color.LimeGreen);
        };

        b5.Click += (_, _) =>
        {
            Log(rtbBlock, "══ ContinueWith [Non-Blocking] ══", Color.Cyan);
            Log(rtbBlock, "🟢 콜백 등록 후 즉시 반환 — 스레드 차단 없음", Color.LimeGreen);
            Task.Run(() => { Thread.Sleep(2000); return 77; })
                .ContinueWith(t => Log(rtbBlock, $"✔ 콜백 실행됨 — 결과: {t.Result}", Color.LimeGreen),
                    TaskScheduler.FromCurrentSynchronizationContext());
            Log(rtbBlock, "이 줄은 ContinueWith 등록 직후 실행됨 (Non-Blocking 증명)", Color.Plum);
        };

        b6.Click += (_, _) =>
        {
            Log(rtbBlock, "══ Deadlock 발생 패턴 (이론 설명) ══", Color.Cyan);
            Log(rtbBlock, "❌ 데드락 유발 코드:", Color.OrangeRed);
            Log(rtbBlock, "   async Task<int> GetAsync() { await Task.Delay(100); return 42; }", Color.Gray);
            Log(rtbBlock, "   int val = GetAsync().Result;  // UI 스레드에서 → 데드락!", Color.OrangeRed);
            Log(rtbBlock, "", Color.Gray);
            Log(rtbBlock, "원인:", Color.SkyBlue);
            Log(rtbBlock, "  1. UI 스레드가 .Result로 자신을 차단", Color.SkyBlue);
            Log(rtbBlock, "  2. await 이후 코드가 UI 스레드에서 실행 필요", Color.SkyBlue);
            Log(rtbBlock, "  3. UI 스레드가 차단돼 있어 재개 불가 → 무한 대기", Color.SkyBlue);
            Log(rtbBlock, "", Color.Gray);
            Log(rtbBlock, "✔ 해결책:", Color.LimeGreen);
            Log(rtbBlock, "  1. await GetAsync()                    ← 가장 권장", Color.LimeGreen);
            Log(rtbBlock, "  2. GetAsync().ConfigureAwait(false)    ← 컨텍스트 미캡처", Color.LimeGreen);
            Log(rtbBlock, "  3. Task.Run(() => GetAsync().Result)   ← 별도 스레드", Color.LimeGreen);
        };

        b7.Click += async (_, _) =>
        {
            Log(rtbBlock, "══ Blocking vs Non-Blocking 성능 비교 ══", Color.Cyan);
            const int N = 10, MS = 100;
            var sw = Stopwatch.StartNew();
            await Task.Run(() => { for (int i = 0; i < N; i++) Thread.Sleep(MS); });
            sw.Stop();
            Log(rtbBlock, $"Blocking  {N}×{MS}ms: {sw.ElapsedMilliseconds}ms (스레드 1개 점유)", Color.OrangeRed);

            sw.Restart();
            await Task.WhenAll(Enumerable.Range(0, N).Select(_ => Task.Delay(MS)));
            sw.Stop();
            Log(rtbBlock, $"Non-Blocking {N}×{MS}ms: {sw.ElapsedMilliseconds}ms (스레드 최소 점유)", Color.LimeGreen);
        };

        bc.Click += (_, _) => rtbBlock.Clear();

        left.Controls.Add(MakeTitle("Blocking vs Non-Blocking"));
        left.Controls.Add(MakeDesc(
            "Blocking   : 결과가 올 때까지 스레드 멈춤\n" +
            "Non-Blocking: 완료를 기다리는 동안 스레드 반환\n\n" +
            "Blocking 예제는 UI 보호를 위해\n" +
            "Task.Run 내부에서 시연합니다."));
        left.Controls.Add(MakeSep());
        left.Controls.AddRange(new Control[] { b1, b2, b3, b4, b5, b6, b7, MakeSep(), bc });

        sc.Panel1.Controls.Add(leftPanel);
        sc.Panel2.Controls.Add(rtbBlock);
        page.Controls.Add(sc);
        return page;
    }

    // ════════════════════════════════════════════════════════
    //  탭 5 : 스레드 동기화
    // ════════════════════════════════════════════════════════
    private TabPage BuildLockTab()
    {
        var page = new TabPage("⑤ 스레드 동기화");
        var sc   = scLock = MakeSplit();
        var (leftPanel, left) = MakeLeft();
        rtbLock  = MakeLog();

        lblCounter = new Label
        {
            Text      = "공유 카운터: ---",
            Font      = new Font("Consolas", 13f, FontStyle.Bold),
            ForeColor = Color.Cyan,
            AutoSize  = false,
            Size      = new Size(276, 30),
            Margin    = new Padding(0, 0, 0, 8),
            TextAlign = ContentAlignment.MiddleLeft,
        };

        var b1 = MakeBtn("1. Race Condition 시연 (동기화 없음)");
        var b2 = MakeBtn("2. lock으로 해결");
        var b3 = MakeBtn("3. Interlocked (원자적 연산)");
        var b4 = MakeBtn("4. Monitor.Enter / Exit");
        var b5 = MakeBtn("5. SemaphoreSlim (동시 접근 수 제한)");
        var b6 = MakeBtn("6. ReaderWriterLockSlim (읽기/쓰기 분리)");
        var b7 = MakeBtn("7. Mutex (프로세스 간 동기화)");
        var bc = MakeBtn("[ 로그 초기화 ]", Color.FromArgb(70, 70, 80));

        void SetCounter(int v)
        {
            if (lblCounter.InvokeRequired) lblCounter.Invoke(() => lblCounter.Text = $"공유 카운터: {v,6:N0}");
            else                            lblCounter.Text = $"공유 카운터: {v,6:N0}";
        }

        // 1. Race Condition
        b1.Click += (_, _) =>
        {
            Log(rtbLock, "══ Race Condition (동기화 없음) ══", Color.Cyan);
            Log(rtbLock, "10 Thread × 1000회 증가 → 기대값: 10,000", Color.SkyBlue);
            Log(rtbLock, "⚠ counter++는 원자적이지 않습니다! [읽기→증가→쓰기] 3단계", Color.OrangeRed);
            int counter = 0;
            SetCounter(0);
            var threads = Enumerable.Range(0, 10).Select(_ => new Thread(() =>
            {
                for (int j = 0; j < 1000; j++) counter++;   // ❌ Race Condition
            })).ToList();
            threads.ForEach(t => t.Start());
            Task.Run(() =>
            {
                threads.ForEach(t => t.Join());
                bool ok = counter == 10_000;
                Log(rtbLock, $"결과: {counter:N0} / 기대: 10,000", ok ? Color.LimeGreen : Color.OrangeRed);
                if (!ok) Log(rtbLock, $"❌ {10_000 - counter:N0}개 손실 (Race Condition 발생!)", Color.Red);
                SetCounter(counter);
            });
        };

        // 2. lock
        b2.Click += (_, _) =>
        {
            Log(rtbLock, "══ lock으로 Race Condition 해결 ══", Color.Cyan);
            Log(rtbLock, "lock은 Monitor.Enter/Exit의 문법적 설탕", Color.SkyBlue);
            int counter = 0;
            SetCounter(0);
            var threads = Enumerable.Range(0, 10).Select(_ => new Thread(() =>
            {
                for (int j = 0; j < 1000; j++) lock (_lock) counter++;   // ✅ 임계 영역 보호
            })).ToList();
            threads.ForEach(t => t.Start());
            Task.Run(() => { threads.ForEach(t => t.Join()); Log(rtbLock, $"✔ 결과: {counter:N0} (정확!)", Color.LimeGreen); SetCounter(counter); });
        };

        // 3. Interlocked
        b3.Click += (_, _) =>
        {
            Log(rtbLock, "══ Interlocked — 원자적(Atomic) 연산 ══", Color.Cyan);
            Log(rtbLock, "lock보다 가볍고 빠름 (CPU 명령 수준 원자성)", Color.SkyBlue);
            int counter = 0;
            SetCounter(0);
            var threads = Enumerable.Range(0, 10).Select(_ => new Thread(() =>
            {
                for (int j = 0; j < 1000; j++) Interlocked.Increment(ref counter);   // ✅ 원자적
            })).ToList();
            threads.ForEach(t => t.Start());
            Task.Run(() =>
            {
                threads.ForEach(t => t.Join());
                Log(rtbLock, $"✔ Interlocked 결과: {counter:N0}", Color.LimeGreen);
                Log(rtbLock, "Add / Exchange / CompareExchange도 활용 가능", Color.SkyBlue);
                SetCounter(counter);
            });
        };

        // 4. Monitor
        b4.Click += (_, _) =>
        {
            Log(rtbLock, "══ Monitor.Enter / Exit ══", Color.Cyan);
            Log(rtbLock, "lock {} 의 내부 구현과 동일, 더 세밀한 제어 가능", Color.SkyBlue);
            int counter = 0;
            var mon = new object();
            SetCounter(0);
            var threads = Enumerable.Range(0, 10).Select(_ => new Thread(() =>
            {
                bool taken = false;
                try { Monitor.Enter(mon, ref taken); for (int j = 0; j < 1000; j++) counter++; }
                finally { if (taken) Monitor.Exit(mon); }
            })).ToList();
            threads.ForEach(t => t.Start());
            Task.Run(() =>
            {
                threads.ForEach(t => t.Join());
                Log(rtbLock, $"✔ Monitor 결과: {counter:N0}", Color.LimeGreen);
                Log(rtbLock, "Monitor.TryEnter / Pulse / Wait으로 고급 패턴 구현 가능", Color.SkyBlue);
                SetCounter(counter);
            });
        };

        // 5. SemaphoreSlim
        b5.Click += (_, _) =>
        {
            Log(rtbLock, "══ SemaphoreSlim — 동시 접근 수 제한 ══", Color.Cyan);
            Log(rtbLock, "10개 Task, 최대 3개만 동시 실행 허용", Color.SkyBlue);
            var sem = new SemaphoreSlim(3, 3);
            var tasks = Enumerable.Range(1, 10).Select(id => Task.Run(async () =>
            {
                Log(rtbLock, $"Task-{id:D2}: 대기 중...", Color.Gray);
                await sem.WaitAsync();
                try
                {
                    Log(rtbLock, $"Task-{id:D2}: 🟢 실행 중 (슬롯 {3 - sem.CurrentCount}/3 사용)", Color.Gold);
                    await Task.Delay(900);
                    Log(rtbLock, $"Task-{id:D2}: ✔ 완료", Color.LimeGreen);
                }
                finally { sem.Release(); }
            })).ToArray();
            Task.WhenAll(tasks).ContinueWith(_ => Log(rtbLock, "모든 Task 완료 (동시성 제한 준수)", Color.LimeGreen));
        };

        // 6. ReaderWriterLockSlim
        b6.Click += (_, _) =>
        {
            Log(rtbLock, "══ ReaderWriterLockSlim ══", Color.Cyan);
            Log(rtbLock, "읽기: 여러 스레드 동시 허용 / 쓰기: 단독 점유", Color.SkyBlue);
            var rwl    = new ReaderWriterLockSlim();
            int shared = 0;
            var readers = Enumerable.Range(1, 4).Select(id => Task.Run(() =>
            {
                for (int j = 0; j < 3; j++)
                {
                    rwl.EnterReadLock();
                    try   { Log(rtbLock, $"Reader-{id}: 읽기 중 (공유값={shared})", Color.DeepSkyBlue); Thread.Sleep(150); }
                    finally { rwl.ExitReadLock(); }
                    Thread.Sleep(80);
                }
            })).ToArray();
            var writers = Enumerable.Range(1, 2).Select(id => Task.Run(() =>
            {
                for (int j = 0; j < 2; j++)
                {
                    rwl.EnterWriteLock();
                    try   { shared += 10; Log(rtbLock, $"Writer-{id}: 📝 쓰기 → 공유값={shared} (단독 점유)", Color.OrangeRed); Thread.Sleep(250); }
                    finally { rwl.ExitWriteLock(); }
                    Thread.Sleep(120);
                }
            })).ToArray();
            Task.WhenAll(readers.Concat(writers)).ContinueWith(_ =>
            {
                Log(rtbLock, $"✔ 완료. 최종 공유값: {shared}", Color.LimeGreen);
                SetCounter(shared);
            });
        };

        // 7. Mutex
        b7.Click += (_, _) =>
        {
            Log(rtbLock, "══ Mutex — 프로세스 간 동기화 ══", Color.Cyan);
            Log(rtbLock, "Mutex는 OS 커널 오브젝트 — 프로세스 경계 초월", Color.SkyBlue);
            Log(rtbLock, "인트라-프로세스 동기화에는 lock이 훨씬 가벼움", Color.Gray);
            var mutex = new Mutex(false, "MultithreadingDemoMutex");
            Enumerable.Range(1, 4).ToList().ForEach(id => new Thread(() =>
            {
                Log(rtbLock, $"Thread-{id}: Mutex 획득 시도...", Color.Gray);
                mutex.WaitOne();
                try   { Log(rtbLock, $"Thread-{id}: 🟢 Mutex 획득, 작업 중 (700ms)", Color.Gold); Thread.Sleep(700); Log(rtbLock, $"Thread-{id}: ✔ 완료", Color.LimeGreen); }
                finally { mutex.ReleaseMutex(); }
            }).Start());
        };

        bc.Click += (_, _) => { rtbLock.Clear(); lblCounter.Text = "공유 카운터: ---"; };

        left.Controls.Add(MakeTitle("스레드 동기화 (Thread Synchronization)"));
        left.Controls.Add(MakeDesc(
            "여러 스레드가 공유 자원에 동시 접근하면\n" +
            "Race Condition → 데이터 손상 발생.\n" +
            "동기화 도구로 임계 영역을 보호합니다."));
        left.Controls.Add(MakeSep());
        left.Controls.Add(lblCounter);
        left.Controls.AddRange(new Control[] { b1, b2, b3, b4, b5, b6, b7, MakeSep(), bc });

        sc.Panel1.Controls.Add(leftPanel);
        sc.Panel2.Controls.Add(rtbLock);
        page.Controls.Add(sc);
        return page;
    }
}
