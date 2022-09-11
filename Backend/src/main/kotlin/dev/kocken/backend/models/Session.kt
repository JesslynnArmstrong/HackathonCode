package dev.kocken.backend.models

import com.google.cloud.Timestamp
import dev.kocken.backend.models.firebase.FirebaseCollectable
import dev.kocken.backend.models.firebase.FirebaseEvent
import dev.kocken.backend.models.firebase.FirebaseSession
import java.time.LocalDateTime

data class Session(
    val documentId: String? = null,
    val userId: String,
    val sessionName: String,
    val startDate: LocalDateTime,
    val endDate: LocalDateTime,
    val collectables: List<Collectable> = emptyList(),
    val events: List<Event> = emptyList(),
)

fun Session.toFirebase(
    userId: String,
    sessionName: String,
    startDate: LocalDateTime,
    endDate: LocalDateTime,
    collectables: List<Collectable>,
    events: List<Event>
): FirebaseSession {

    val newEvents = mutableListOf<FirebaseEvent>()
    val newCollectables = mutableListOf<FirebaseCollectable>()

    events.forEach { event ->
        newEvents.add(event.toFirebase(event.name, event.timestamp, event.value))
    }

    collectables.forEach { collectable ->
        newCollectables.add(collectable.toFirebase(collectable.name, collectable.amount))
    }

    val startDateTimestamp = Timestamp.parseTimestamp(startDate.toString())
    val endDateTimestamp = Timestamp.parseTimestamp(endDate.toString())

    return FirebaseSession("", userId, sessionName, startDateTimestamp, endDateTimestamp, newCollectables, newEvents)
}

