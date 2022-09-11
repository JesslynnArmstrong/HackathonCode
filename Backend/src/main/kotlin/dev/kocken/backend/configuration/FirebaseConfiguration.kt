package dev.kocken.backend.configuration

import com.google.auth.oauth2.GoogleCredentials
import com.google.cloud.firestore.Firestore
import com.google.firebase.FirebaseApp
import com.google.firebase.FirebaseOptions
import com.google.firebase.cloud.FirestoreClient
import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration
import org.springframework.core.io.ClassPathResource

@Configuration
class FirebaseConfiguration {

    val app: FirebaseApp = FirebaseApp.initializeApp(
        FirebaseOptions.builder().setCredentials((GoogleCredentials.fromStream(ClassPathResource("service-account.json").inputStream))).build()
    )

    @Bean
    fun firestore(): Firestore {
        return FirestoreClient.getFirestore(app)
    }

}
