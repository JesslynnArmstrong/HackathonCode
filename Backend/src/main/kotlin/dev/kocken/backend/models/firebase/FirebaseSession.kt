package dev.kocken.backend.models.firebase

import com.google.cloud.Timestamp
import dev.kocken.backend.models.Collectable
import dev.kocken.backend.models.Event
import dev.kocken.backend.models.Session
import java.time.LocalDateTime
import java.time.ZoneId

data class FirebaseSession(
    val documentId: String,
    val userId: String,
    val sessionName: String,
    val startDate: Timestamp,
    val endDate: Timestamp,
    val collectables: List<FirebaseCollectable> = emptyList(),
    val events: List<FirebaseEvent> = emptyList(),
) {
    constructor() : this("", "", "", Timestamp.now(), Timestamp.now(), emptyList(), emptyList())
}

fun FirebaseSession.convert(
    documentId: String,
    userId: String,
    sessionName: String,
    startDate: Timestamp,
    endDate: Timestamp,
    collectables: List<FirebaseCollectable>,
    events: List<FirebaseEvent>
): Session {

    val newStartDate: LocalDateTime = startDate.toDate().toInstant().atZone(ZoneId.systemDefault()).toLocalDateTime()
    val newEndDate: LocalDateTime = endDate.toDate().toInstant().atZone(ZoneId.systemDefault()).toLocalDateTime()

    val newEvents = mutableListOf<Event>()
    val newCollectables = mutableListOf<Collectable>()

    events.forEach { event ->
        newEvents.add(event.convert(event.name, event.timestamp, event.value))
    }

    collectables.forEach { collectable ->
        newCollectables.add(collectable.convert(collectable.name, collectable.amount))
    }

    return Session(documentId, userId, sessionName, newStartDate, newEndDate, newCollectables, newEvents)
}
