## 1. 在单元测试项目上安装coverlet.msbuild nuget包：
    dotnet add package coverlet.msbuild
这个包是为了在下面运行中的/p:CollectCoverage=true能够直接在命令行中显示代码覆盖率，检查coverlet.collector包是否存在, 如果不存在，也需要安装。

## 2. 运行测试
    dotnet test /p:CollectCoverage=true --collect "XPlat Code Coverage" --results-directory C:\temp\TestResults

## 3. 使用reportgenerator生成报告
需要预先安装reportgenerator工具：

    dotnet tool install -g dotnet-reportgenerator-globaltool

运行报告前找到testresults目录下的coverage.cobertura.xml, 复制对应路径执行以下命令

    reportgenerator -reports:"C:\temp\TestResults\5f7e3230-b871-4214-8f53-bb2ca3603ca7\coverage.cobertura.xml" -targetdir:"C:\temp\coverage" -reporttypes:Html

## 4. 在targetdir目录中打开index.html
