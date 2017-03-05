"""
Django settings for showdown project.

For more information on this file, see
https://docs.djangoproject.com/en/1.9/topics/settings/

For the full list of settings and their values, see
https://docs.djangoproject.com/en/1.9/ref/settings/
"""

import environ

ROOT_DIR = environ.Path(__file__) - 3
APPS_DIR = ROOT_DIR.path('showdown')

env = environ.Env()

# App Configuration
DJANGO_APPS = [
    'django.contrib.admin',
    'django.contrib.auth',
    'django.contrib.contenttypes',
    'django.contrib.sessions',
    'django.contrib.messages',
    'django.contrib.staticfiles',
]

THIRD_PARTY_APPS = [
    'graphene_django',
]

LOCAL_APPS = [
    'core',
]

INSTALLED_APPS = DJANGO_APPS + THIRD_PARTY_APPS + LOCAL_APPS


# Middleware Configuration
MIDDLEWARE_CLASSES = [
    'django.middleware.security.SecurityMiddleware',
    'django.contrib.sessions.middleware.SessionMiddleware',
    'django.middleware.common.CommonMiddleware',
    'django.middleware.csrf.CsrfViewMiddleware',
    'django.contrib.auth.middleware.AuthenticationMiddleware',
    'django.contrib.auth.middleware.SessionAuthenticationMiddleware',
    'django.contrib.messages.middleware.MessageMiddleware',
    'django.middleware.clickjacking.XFrameOptionsMiddleware',
]

# Debug
# --------------------------------------------------------------------------
DEBUG = env.bool('DJANGO_DEBUG', False)

# Database Configuration
# --------------------------------------------------------------------------
DATABASES = {
    'default': env.db('DJANGO_DATABASE_URL'),
}
DATABASES['default']['ATOMIC_REQUESTS'] = True

# General Configuration
# --------------------------------------------------------------------------
LANGUAGE_CODE = 'en-us'

TIME_ZONE = 'America/Chicago'

USE_I18N = True

USE_L10N = True

USE_TZ = True

# Template Configuration
# --------------------------------------------------------------------------

TEMPLATES = [
    {
        'BACKEND': 'django.template.backends.django.DjangoTemplates',
        'DIRS': [
            str(APPS_DIR.path('templates')),
        ],
        'OPTIONS': {
            'debug': DEBUG,
            'loaders': [
                'django.template.loaders.filesystem.Loader',
                'django.template.loaders.app_directories.Loader',
            ],
            'context_processors': [
                'django.template.context_processors.debug',
                'django.template.context_processors.request',
                'django.contrib.auth.context_processors.auth',
                'django.contrib.messages.context_processors.messages',
            ],
        },
    },
]

# Static File Configuration
# --------------------------------------------------------------------------
STATIC_ROOT = str(ROOT_DIR('staticfiles'))
STATIC_URL = '/static/'
# STATICFILES_DIRS = (
#     str(APPS_DIR.path('static')),
# )

STATICFILES_FINDERS = (
    'django.contrib.staticfiles.finders.FileSystemFinder',
    'django.contrib.staticfiles.finders.AppDirectoriesFinder',
)
# URL Configuration
# --------------------------------------------------------------------------
ROOT_URLCONF = 'config.urls'

WSGI_APPLICATION = 'config.wsgi.application'

# Authentication Configuration
# --------------------------------------------------------------------------
# Password validation
# https://docs.djangoproject.com/en/1.9/ref/settings/#auth-password-validators

AUTH_PASSWORD_VALIDATORS = [
    {
        'NAME': 'django.contrib.auth.password_validation.UserAttributeSimilarityValidator',
    },
    {
        'NAME': 'django.contrib.auth.password_validation.MinimumLengthValidator',
    },
    {
        'NAME': 'django.contrib.auth.password_validation.CommonPasswordValidator',
    },
    {
        'NAME': 'django.contrib.auth.password_validation.NumericPasswordValidator',
    },
]

SECRET_KEY = env('DJANGO_SECRET_KEY')

# Logging Configuration
# -------------------------------------------------------------------------
LOGGING = {
    'version': 1,
    'disable_existing_loggers': False,
    'handlers': {
        'console': {
            'class': 'logging.StreamHandler',
            'formatter': 'color',
        },
    },
        
    'formatters': {
        'color': {
            '()': 'colorlog.ColoredFormatter',
            'format': '%(log_color)s%(levelname)s %(asctime)s %(name)s:%(lineno)s %(message)s',
            'log_colors': {
                'DEBUG': 'cyan',
                'INFO': 'green',
                'WARNING': 'yellow',
                'ERROR': 'red',
                'CRITICAL': 'bold_red',
            }
        }
    },
    'loggers': {
        'showdown': {
            'handlers': ['console'],
            'level': env('DJANGO_LOG_LEVEL', default='INFO')
        }
    }
}

GRAPHENE = {
    'SCHEMA': 'core.schema.schema'
}

FACEBOOK = {
    'app_id': env('FACEBOOK_APP_ID'),
    'app_secret': env('FACEBOOK_APP_SECRET'),
}