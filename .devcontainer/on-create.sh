#!/bin/sh

echo "on-create start" >> $HOME/status

# run dotnet restore
dotnet restore src

echo "on-create complete" >> $HOME/status
