package dev.kocken.backend.models

import com.google.cloud.Timestamp
import dev.kocken.backend.models.firebase.FirebaseEvent
import java.time.LocalDateTime

data class Event(
    val name: String,
    val timestamp: LocalDateTime,
    val value: Number,
)

fun Event.toFirebase(
    name: String, timestamp: LocalDateTime, value: Number
): FirebaseEvent {
    return FirebaseEvent(name, Timestamp.parseTimestamp(timestamp.toString()), value.toString())
}
