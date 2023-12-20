

# Fun��o para matar processos com base no nome do processo
function Stop-ProcessesByName {
    $processes = Get-Process | Where-Object { $_.ProcessName -like 'dotnet*' } | Where-Object { $_.MainWindowTitle -eq '' }
    if ($processes.Count -gt 0) {
        $processes | ForEach-Object { Stop-Process -Id $_.Id -Force }
    }
}

# Encerra qualquer processo em segundo plano relacionado ao comando npm run test:watch
Stop-ProcessesByName


# Pasta onde o relat�rio ser� gerado
$reportPath = ".\xunit.test.project\TestResults"

# Exclui todo o conte�do da pasta TestResults, se existir
if (Test-Path $reportPath) {
    Remove-Item -Recurse -Force $reportPath
}

# Executa o teste e coleta o GUID gerado
dotnet clean
dotnet build --restore
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover"

# Encontra o diret�rio mais recente na pasta TestResults
$latestDir = Get-ChildItem -Directory -Path .\xunit.test.project\TestResults | Sort-Object LastWriteTime -Descending | Select-Object -First 1
$sourceDirs = Join-Path -Path (Get-Location) -ChildPath "Domain"; Join-Path -Path (Get-Location) -ChildPath "Repository"

# Verifica se encontrou um diret�rio e, em caso afirmativo, obt�m o nome do diret�rio (GUID)
if ($latestDir -ne $null) {
    $guid = $latestDir.Name
  
    # Constr�i os caminhos dinamicamente
    $baseDirectory = Join-Path -Path (Get-Location) -ChildPath "xunit.test.project"
    $coverageXmlPath = Join-Path -Path (Join-Path -Path $baseDirectory -ChildPath "TestResults") -ChildPath $guid

    # Gera o relat�rio de cobertura usando o GUID capturado
    reportgenerator -reports:$baseDirectory\coverage.cobertura.xml -targetdir:$coverageXmlPath\coveragereport -reporttypes:"Html;lcov;" -sourcedirs:$sourceDirs
    

    # Abre a p�gina index.html no navegador padr�o do sistema operacional
    Invoke-Item $coverageXmlPath\coveragereport\index.html
}
else {
    Write-Host "Nenhum diret�rio de resultados encontrado."
} 