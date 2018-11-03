<template>
    <div>
        <v-toolbar flat color="white">
            <v-toolbar-title>Users</v-toolbar-title>
            <v-divider class="mx-2"
                       inset
                       vertical>
            </v-divider>
            <v-spacer></v-spacer>
            <v-text-field v-model="search"
                          append-icon="search"
                          label="Search"
                          single-line
                          hide-details>
            </v-text-field>
            <v-spacer></v-spacer>
        </v-toolbar>

        <v-data-table :headers="headers"
                      :items="pagedListOfUserListDto.items"
                      :pagination.sync="pagination"
                      :search="search"
                      :total-items="pagedListOfUserListDto.totalCount"
                      :loading="loading"
                      class="elevation-1">
            <template slot="items" slot-scope="props">
                <td>{{ props.item.userName }}</td>
                <td>{{ props.item.email }}</td>
                <td class="justify-center layout px-0">
                    <v-icon v-if="!isAdminUser(props.item.userName)" small
                            @click="deleteRole(props.item.id)">
                        delete
                    </v-icon>
                </td>
            </template>
            <template slot="no-data" v-if="!loading">
                <v-alert :value="true" color="error" icon="warning">
                    Sorry, nothing to display here :(
                </v-alert>
            </template>
        </v-data-table>
    </div>
</template>

<script src="./user-list.ts"></script>
