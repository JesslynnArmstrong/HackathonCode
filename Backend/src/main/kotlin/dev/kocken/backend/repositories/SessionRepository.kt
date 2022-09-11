package dev.kocken.backend.repositories

import com.google.cloud.firestore.Firestore
import dev.kocken.backend.models.Session
import dev.kocken.backend.models.firebase.FirebaseSession
import dev.kocken.backend.models.firebase.convert
import dev.kocken.backend.models.toFirebase
import org.springframework.stereotype.Service

@Service
class SessionRepository(private val firestore: Firestore) {

    private val sessionCollection = "sessions"

    fun getSessions(): List<Session> {
        val sessions = mutableListOf<Session>()

        val query = firestore.collection(sessionCollection).get()
        val documents = query.get().documents

        documents.forEach { document ->
            val firebaseSession = document.toObject(FirebaseSession::class.java)
            val convertedSession = firebaseSession.convert(
                firebaseSession.documentId,
                firebaseSession.userId,
                firebaseSession.sessionName,
                firebaseSession.startDate,
                firebaseSession.endDate,
                firebaseSession.collectables,
                firebaseSession.events
            )
            sessions.add(convertedSession)
        }
        return sessions
    }

    fun getSession(id: String): Session {
        val query = firestore.collection(sessionCollection).whereEqualTo("documentId", id).get()

        val document = query.get().first()
        val session = document.toObject(FirebaseSession::class.java)

        return session.convert(
            session.documentId,
            session.userId,
            session.sessionName,
            session.startDate,
            session.endDate,
            session.collectables,
            session.events
        )
    }

    fun createSession(session: Session) {

        val newSession = session.toFirebase(
            session.userId,
            session.sessionName,
            session.startDate,
            session.endDate,
            session.collectables,
            session.events
        )

        val newDocument = firestore.collection(sessionCollection).add(newSession)
        val documentId = newDocument.get().id

        firestore.collection(sessionCollection).document(documentId).update("documentId", documentId)
        return
    }

    fun deleteSession(id: String) {
        firestore.collection(sessionCollection).document(id).delete()
        return
    }

}
