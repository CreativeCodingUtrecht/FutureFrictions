<script lang="ts">
  	import { TabGroup, TabAnchor } from '@skeletonlabs/skeleton';
  	import { page } from '$app/stores';
        
    export let data: PageData;
    
    const scenario = data.scenario;

    const url_step0 = `/scenarios/${scenario}/steps/0-setup`
    const url_step1 = `/scenarios/${scenario}/steps/1-friction`
    const url_step2 = `/scenarios/${scenario}/steps/2-present`
    const url_step3 = `/scenarios/${scenario}/steps/3-whatif`
    const url_step4 = `/scenarios/${scenario}/steps/4-future`
    const url_step5 = `/scenarios/${scenario}/steps/5-reflect`
</script>

<TabGroup>
	<!-- <TabAnchor href="/scenarios/{scenario}" selected={$page.url.pathname === `/scenarios/${scenario}`}>Summary</TabAnchor>
  <TabAnchor href="/scenarios/{scenario}/edit" selected={$page.url.pathname === `/scenarios/${scenario}/edit`}>Definition</TabAnchor>  
	<TabAnchor href="/scenarios/{scenario}/images" selected={$page.url.pathname === `/scenarios/${scenario}/images`}>Images</TabAnchor> -->
  <!-- <TabAnchor href={url} target="FF_WEBGL_URL">Play</TabAnchor> -->
  <TabAnchor href={url_step0} selected={$page.url.pathname === url_step0}><b>0</b>. Before we start</TabAnchor>  
  <TabAnchor href={url_step1} selected={$page.url.pathname === url_step1}><b>1</b>. Friction</TabAnchor>  
  <TabAnchor href={url_step2} selected={$page.url.pathname === url_step2}><b>2</b>. Present context</TabAnchor>  
  <TabAnchor href={url_step3} selected={$page.url.pathname === url_step3}><b>3</b>. What if ..</TabAnchor>  
  <TabAnchor href={url_step4} selected={$page.url.pathname === url_step4}><b>4</b>. Back to the future</TabAnchor>  
  <TabAnchor href={url_step5} selected={$page.url.pathname === url_step5}><b>5</b>. Reflection</TabAnchor>  
</TabGroup>

<slot />