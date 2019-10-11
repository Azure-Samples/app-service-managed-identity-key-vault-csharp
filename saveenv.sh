#!/bin/bash

if [ -z "$mikv_Name" ]
then
  echo "Please set mikv_Name before running this script"
else
  if [ -f ~/${mikv_Name}.env ]
  then
    if [ "$#" = 0 ] || [ $1 != "-y" ]
    then
      read -p "~/${mikv_Name}.env already exists. Do you want to remove? (y/n) " response

      if ! [[ $response =~ [yY] ]]
      then
        echo "Please move or delete ~/${mikv_Name}.env and rerun the script."
        exit 1;
      fi
    fi
  fi

  echo '#!/bin/bash' > ~/${mikv_Name}.env
  echo '' >> ~/${mikv_Name}.env

  for var in $(env | grep mikv_ | sort)
  do
    echo "export ${var}" >> ~/${mikv_Name}.env
  done

  cat ~/${mikv_Name}.env
fi
