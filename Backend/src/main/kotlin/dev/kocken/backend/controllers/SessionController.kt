package dev.kocken.backend.controllers

import dev.kocken.backend.models.Session
import dev.kocken.backend.services.SessionService
import org.springframework.http.HttpStatus
import org.springframework.http.ResponseEntity
import org.springframework.web.bind.annotation.*

@RestController
@RequestMapping("/session")
class SessionController(private val sessionService: SessionService) {

    @GetMapping("/all")
    fun getSessions(): ResponseEntity<List<Session>> {
        return ResponseEntity(sessionService.getSessions(), HttpStatus.OK)
    }

    @GetMapping("/{id}")
    fun getSession(@PathVariable id: String): ResponseEntity<Session> {
        return ResponseEntity(sessionService.getSession(id), HttpStatus.OK)
    }

    @PostMapping("/create")
    fun createSession(@RequestBody session: Session): ResponseEntity<Unit> {
        return ResponseEntity(sessionService.createSession(session), HttpStatus.OK)
    }

    @DeleteMapping("/delete/{id}")
    fun deleteSession(@PathVariable id: String): ResponseEntity<Unit> {
        return ResponseEntity(sessionService.deleteSession(id), HttpStatus.OK)
    }

}
