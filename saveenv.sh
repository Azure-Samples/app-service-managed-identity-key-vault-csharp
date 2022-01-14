#!/bin/bash

if [ -z "$MIKV_NAME" ]
then
  echo "Please set MIKV_NAME before running this script"
else
  if [ -f ~/${MIKV_NAME}.env ]
  then
    if [ "$#" = 0 ] || [ $1 != "-y" ]
    then
      read -p "~/${MIKV_NAME}.env already exists. Do you want to remove? (y/n) " response

      if ! [[ $response =~ [yY] ]]
      then
        echo "Please move or delete ~/${MIKV_NAME}.env and rerun the script."
        exit 1;
      fi
    fi
  fi

  echo '#!/bin/bash' > ~/${MIKV_NAME}.env
  echo '' >> ~/${MIKV_NAME}.env

  for var in $(env | grep MIKV_ | sort)
  do
    echo "export ${var}" >> ~/${MIKV_NAME}.env
  done

  cat ~/${MIKV_NAME}.env
fi
