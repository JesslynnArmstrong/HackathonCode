package dev.kocken.backend.models

import dev.kocken.backend.models.firebase.FirebaseCollectable

data class Collectable(
    val name: String,
    val amount: Number,
)

fun Collectable.toFirebase(
    name: String,
    amount: Number,
): FirebaseCollectable {
    return FirebaseCollectable(name, amount.toString())
}
