export SV_ADDRESS="http://localhost" #server external address

export SLIB_ISSUER="dytsenayasar.com"
export SLIB_AUTH_SECRET="secret123"
export SLIB_FILE_REQ_SECRET="secret1234" #have to be same with android project.
export SLIB_FILE_MAX_SIZE=250 #MB
export SLIB_IMG_MAX_SIZE=5 #MB

export SLIB_AUTH_TIME_MIN=30 #MIN
export SLIB_EXT_PORT=5000
export SLIB_EXT_ADDRESS="$SV_ADDRESS:$SLIB_EXT_PORT"
export SLIB_FRONT_EXT_PORT=3000

export SLIB_CORS_ADDRESS="*"

export PG_USER="db_user"
export PG_PASSWORD="database_pass"
export PG_CONNECTION_STR="Host=postgres;Database=dytsenayasar;Username=$PG_USER;Password=$PG_PASSWORD"

export SMTP_SENDER="DytSenaYasar"
export SMTP_SV="smtp.gmail.com"
export SMTP_PORT=587
export SMTP_USER_NAME="test@mail.com"
export SMTP_USER_PASS="mail_pass"

#Graylog log collector config
export GL_EXT_PORT=9000
export GL_EXT_ADDRESS="$SV_ADDRESS:$GL_EXT_PORT/"
export GL_PASSWORD_SECRET="!*?WNngzctGEX2~q%@S-dxb/pj7FN&)2c~^%Pd3a"
export GL_PASSWORD_HASH="4r8d63cc5c04d0eb3f72633343e2d57d438500914e1a1d1867414a0a4111fbc7" #password as sha256 hash

