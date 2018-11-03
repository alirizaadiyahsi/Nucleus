<template>
    <div>
        <h1>Counter</h1>

        <p>This is a simple example of a Vue.js component.</p>

        <p>Current count: <strong>{{ currentcount }}</strong></p>

        <v-btn color="primary" @click="incrementCounter">Increment</v-btn>
    </div>
</template>

<script src="./counter.ts"></script>
