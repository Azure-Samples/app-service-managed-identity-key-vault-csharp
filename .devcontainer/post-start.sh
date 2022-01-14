#!/bin/bash

echo "post-start start" >> $HOME/status

# this runs each time the container starts

# run dotnet restore
dotnet restore src

echo "post-start complete" >> $HOME/status
