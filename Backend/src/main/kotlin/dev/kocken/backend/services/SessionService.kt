package dev.kocken.backend.services

import dev.kocken.backend.models.Session
import dev.kocken.backend.repositories.SessionRepository
import org.springframework.stereotype.Service


@Service
class SessionService(private val sessionRepository: SessionRepository) {

    fun getSessions(): List<Session> {
        return sessionRepository.getSessions()
    }

    fun getSession(id: String): Session {
        return sessionRepository.getSession(id)
    }

    fun createSession(session: Session) {
        return sessionRepository.createSession(session)
    }

    fun deleteSession(id: String) {
        return sessionRepository.deleteSession(id)
    }

}
