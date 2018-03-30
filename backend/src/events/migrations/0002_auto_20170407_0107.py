# -*- coding: utf-8 -*-
# Generated by Django 1.10.6 on 2017-04-07 06:07
from __future__ import unicode_literals

from django.db import migrations

from dateutil import tz
import arrow

def datetime(day, hour, minute):
    return arrow.get(2018, 4 if (day == 1) else 3, day, hour, minute, 0, 0, tz.gettz('America/Chicago')).datetime

def generate_seed_data(apps, schema_editor):
    Location = apps.get_model("events", "Location")
    db_alias = schema_editor.connection.alias

    union_ballroom = Location(
        name="Texas Union: Shirley Bird Perry Ballroom",
        address="Texas Union Building Austin, Texas",
        latitude=30.2866603, longitude=-97.7411629,
        notes=""
    )
    union_quadrangle = Location(
        name="Texas Union: Quadrangle Room",
        address="Texas Union Building Austin, Texas",
        latitude=30.2866603, longitude=-97.7411629,
        notes=""
    )
    union_eastwoods = Location(
        name="Texas Union: Eastwoods Room",
        address="Texas Union Building Austin, Texas",
        latitude=30.2866603, longitude=-97.7411629,
        notes=""
    )
    nueces_mosque = Location(
        name="Nueces Mosque",
        address="Nueces Mosque, 1906 Nueces St, Austin, TX 78705",
        latitude=30.2831868, longitude=-97.7443387,
        notes=""
    )
    gregory_gym = Location(
        name="Gregory Gym: Arena 3.108",
        address="Gregory Gymnasium, 2101 Speedway, Austin, TX 78712",
        latitude=30.2844637, longitude=-97.7358777,
        notes=""
    )
    rec_sports = Location(
        name="Recreational Sports Center: Room 2.200",
        address="Recreational Sports Center, 2001 San Jacinto Blvd, Austin, TX 78705",
        latitude=30.2815045, longitude=-97.732335,
        notes=""
    )
    clark_basketball = Location(
        name="Clark Basketball Courts",
        address="Clark Basketball Courts, Basketball Support Building, 301 Jester Cir, Austin, TX 78712",
        latitude=30.2811546, longitude=-97.7355434,
        notes=""
    )
    whitaker_fields = Location(
        name="Wright-Whitaker Fields",
        address="Charles Alan Wright Fields, 4901 Guadalupe St, Austin, TX 78751",
        latitude=30.3161402, longitude=-97.7270667,
        notes=""
    )
    main_mall_tower = Location(
        name="Main Mall: UT Tower",
        address="Main Building, 110 Inner Campus Drive, Austin, TX 78705",
        latitude=30.2861062, longitude=-97.7393634,
        notes=""
    )
    utc_2_102A = Location(
        name="University Teaching Center: Room 2.102A",
        address="University Teaching Center, Austin, TX 78705",
        latitude=30.2830485, longitude=-97.7388064,
        notes=""
    )
    utc_2_112A = Location(
        name="University Teaching Center: Room 2.112A",
        address="University Teaching Center, Austin, TX 78705",
        latitude=30.2830485, longitude=-97.7388064,
        notes=""
    )
    utc_1_102 = Location(
        name="University Teaching Center: Room 1.102",
        address="University Teaching Center, Austin, TX 78705",
        latitude=30.2830485, longitude=-97.7388064,
        notes=""
    )
    utc_1_130 = Location(
        name="University Teaching Center: Room 1.130",
        address="University Teaching Center, Austin, TX 78705",
        latitude=30.2830485, longitude=-97.7388064,
        notes=""
    )
    utc_3_102 = Location(
        name="University Teaching Center: Room 3.102",
        address="University Teaching Center, Austin, TX 78705",
        latitude=30.2830485, longitude=-97.7388064,
        notes=""
    )
    utc_3_112 = Location(
        name="University Teaching Center: Room 3.112",
        address="University Teaching Center, Austin, TX 78705",
        latitude=30.2830485, longitude=-97.7388064,
        notes=""
    )
    utc_4_112 = Location(
        name="University Teaching Center: Room 4.112",
        address="University Teaching Center, Austin, TX 78705",
        latitude=30.2830485, longitude=-97.7388064,
        notes=""
    )
    jester = Location(
        name="Jester",
        address="Jester West Residence Hall, 201 E 21st St, Austin, TX 78705",
        latitude=30.2819983, longitude=-97.7372298,
        notes=""
    )
    sports = Location(
        name="Rec Sports (Sisters)/Wright-Whitaker Fields (Brothers)",
        address="Recreational Sports Center, 2001 San Jacinto Blvd, Austin, TX 78705",
        latitude=30.2815045, longitude=-97.732335,
       notes=""
    )
    sat_registration = Location(
        name="Texas Union Ballroom/Gregory Gym/Rec Sports",
        address="Texas Union Building Austin, Texas",
        latitude=30.2866603, longitude=-97.7411629,
        notes=""
    )
    sat_lunch = Location(
        name="Gregory Gym (Brothers)/Rec Sports (Sisters)",
        address="Gregory Gymnasium, 2101 Speedway, Austin, TX 78712",
        latitude=30.2844637, longitude=-97.7358777,
        notes=""
    )

    Location.objects.using(db_alias).bulk_create([
        union_ballroom, union_quadrangle,
        union_eastwoods, nueces_mosque, rec_sports, gregory_gym,
        main_mall_tower, whitaker_fields,
        utc_2_102A, clark_basketball,
        utc_2_112A, utc_1_102, utc_1_130, utc_3_102, 
        utc_3_112, utc_4_112, jester, sports, 
        sat_registration, sat_lunch
    ])

class Migration(migrations.Migration):

    dependencies = [
        ('events', '0001_initial'),
    ]

    operations = [
        migrations.RunPython(generate_seed_data),
    ]
