export SV_ADDRESS="http://localhost" #server external address

export DYT_ISSUER="dytsenayasar.com"
export DYT_AUTH_SECRET="secret123"
export DYT_FILE_MAX_SIZE=250 #MB
export DYT_IMG_MAX_SIZE=5 #MB

export DYT_AUTH_TIME_MIN=30 #MIN
export DYT_EXT_PORT=5000
export DYT_EXT_ADDRESS="$SV_ADDRESS:$DYT_EXT_PORT"
export DYT_FRONT_EXT_PORT=3000

export DYT_CORS_ADDRESS="*"

export PG_USER="db_user"
export PG_PASSWORD="database_pass"
export PG_CONNECTION_STR="Host=postgres;Database=dytsenayasar;Username=$PG_USER;Password=$PG_PASSWORD"

export SMTP_SENDER="Diyetisyen Sena Yaşar"
export SMTP_SV="smtp.gmail.com"
export SMTP_PORT=587
export SMTP_USER_NAME="test@mail.com"
export SMTP_USER_PASS="mail_pass"

