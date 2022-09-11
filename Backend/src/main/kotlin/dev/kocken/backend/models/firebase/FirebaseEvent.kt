package dev.kocken.backend.models.firebase

import com.google.cloud.Timestamp
import dev.kocken.backend.models.Event
import java.time.LocalDateTime
import java.time.ZoneId

data class FirebaseEvent(
    val name: String,
    val timestamp: Timestamp,
    val value: String,
) {
    constructor() : this("", Timestamp.now(), "")
}

fun FirebaseEvent.convert(
    name: String, timestamp: Timestamp, value: String
): Event {
    val localDateTime: LocalDateTime = timestamp.toDate().toInstant().atZone(ZoneId.systemDefault()).toLocalDateTime()
    val newValue = value.toInt()
    return Event(name, localDateTime, newValue)
}
