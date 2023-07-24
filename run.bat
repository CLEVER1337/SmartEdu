@echo off

docker-compose up -d

start https://localhost:32222

pause

for /f "usebackq tokens=*" %%a in (`docker ps -q -a`) do docker rm -f %%a
