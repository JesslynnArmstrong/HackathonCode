package dev.kocken.backend.models.firebase

import dev.kocken.backend.models.Collectable


data class FirebaseCollectable(
    val name: String,
    val amount: String,
) {
    constructor() : this("", "")
}

fun FirebaseCollectable.convert(
    name: String,
    amount: String,
): Collectable {
    return Collectable(name, amount.toInt())
}
