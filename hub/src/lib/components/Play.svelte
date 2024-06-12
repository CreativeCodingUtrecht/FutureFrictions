<script lang="ts">
	import type { ModalSettings, ModalStore } from '@skeletonlabs/skeleton';
	import { getModalStore } from '@skeletonlabs/skeleton';
    import { page } from '$app/stores'

	import { FF_WEBGL_PATH, FF_WEBGL_URL } from '$lib/constants';
	import { onMount } from 'svelte';

	export let scenario: string;
	export let button: boolean = true;

	const modalStore: ModalStore = getModalStore();

	function modalPlay(): void {
		const modal: ModalSettings = {
			type: 'alert',
			buttonTextCancel: 'Close',
            modalClasses: '',
			body: ` 
				<iframe allowfullscreen id="iframe" height="650" class="w-full" src="${FF_WEBGL_URL}?scenario=${scenario}" />
            `
		};
		modalStore.trigger(modal);
		// modal.onOpen = () => {
		// 	console.log('Modal opened');
		// 	var frame = document.getElementById("iframe"); 
		// 	console.log("Frame:", frame)
		// 	frame.onload = function() {
		// 		frame.style.height = frame.contentWindow.document.body.scrollHeight + 'px'; 
		// 		frame.style.width = frame.contentWindow.document.body.scrollWidth+'px'; 						
		// 	}

		// };
    }
</script>

{#if button}
	<a on:click={modalPlay} class="btn variant-filled-primary cursor-pointer">▶ Play</a>	
{:else}
	<a on:click={modalPlay} class="text-surface-500 cursor-pointer">▶</a>
{/if}

