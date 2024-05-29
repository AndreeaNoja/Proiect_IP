plugins {
    alias(libs.plugins.androidApplication)
}

android {
    namespace = "com.example.medconnect_"
    compileSdk = 34

    defaultConfig {
        applicationId = "com.example.medconnect_"
        minSdk = 26
        targetSdk = 34
        versionCode = 1
        versionName = "1.0"

        testInstrumentationRunner = "androidx.test.runner.AndroidJUnitRunner"
    }

    buildTypes {
        release {
            isMinifyEnabled = false
            proguardFiles(getDefaultProguardFile("proguard-android-optimize.txt"), "proguard-rules.pro")
        }
    }
    compileOptions {
        sourceCompatibility = JavaVersion.VERSION_1_8
        targetCompatibility = JavaVersion.VERSION_1_8
    }
}

dependencies {

    implementation(libs.appcompat)
    implementation(libs.material)
    implementation(libs.activity)
    implementation(libs.constraintlayout)
    implementation(libs.legacy.support.v4)
    implementation(libs.google.material)
    testImplementation(libs.junit)
    androidTestImplementation(libs.ext.junit)
    androidTestImplementation(libs.espresso.core)

    implementation ("org.postgresql:postgresql:42.2.9")

    implementation("net.sourceforge.jtds:jtds:1.3.1")

    implementation("com.microsoft.sqlserver:mssql-jdbc:10.2.0.jre8")


}