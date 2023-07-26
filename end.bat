@echo off

for /f "usebackq tokens=*" %%a in (`docker ps -q -a`) do docker rm -f %%a
